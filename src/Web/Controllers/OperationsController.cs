using Web.Models;
using Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public sealed class OperationsController : Controller
{
    private static readonly IReadOnlyCollection<string> PaymentMethods =
    [
        "Card",
        "Cash",
        "Bank Transfer"
    ];

    private readonly GetOperationsDashboardUseCase _dashboard;
    private readonly CreateOrderForTableUseCase _createOrder;
    private readonly AddOrderItemUseCase _addOrderItem;
    private readonly RemoveOrderItemUseCase _removeOrderItem;
    private readonly SendOrderToKitchenUseCase _sendToKitchen;
    private readonly ProcessPaymentUseCase _processPayment;
    private readonly CloseOrderUseCase _closeOrder;

    public OperationsController(
        GetOperationsDashboardUseCase dashboard,
        CreateOrderForTableUseCase createOrder,
        AddOrderItemUseCase addOrderItem,
        RemoveOrderItemUseCase removeOrderItem,
        SendOrderToKitchenUseCase sendToKitchen,
        ProcessPaymentUseCase processPayment,
        CloseOrderUseCase closeOrder)
    {
        _dashboard = dashboard;
        _createOrder = createOrder;
        _addOrderItem = addOrderItem;
        _removeOrderItem = removeOrderItem;
        _sendToKitchen = sendToKitchen;
        _processPayment = processPayment;
        _closeOrder = closeOrder;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var dashboard = await _dashboard.ExecuteAsync(DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
        var viewModel = new OperationsDashboardViewModel(dashboard, PaymentMethods);
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateOrder(CreateOrderInputModel input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            SetFlash("error", "Select a valid table before opening a ticket.");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var order = await _createOrder.ExecuteAsync(input.TableNumber, cancellationToken);
            SetFlash("success", $"Opened ticket {ShortId(order.Id)} for table {order.TableNumber}.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddItem(AddOrderItemInputModel input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            SetFlash("error", "Provide a valid menu item and quantity.");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var order = await _addOrderItem.ExecuteAsync(input.OrderId, input.Sku, input.Name, input.Quantity, input.UnitPrice, cancellationToken);
            SetFlash("success", $"Added {input.Quantity} x {input.Name} to table {order.TableNumber}.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveItem(RemoveOrderItemInputModel input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            SetFlash("error", "Provide a valid quantity to remove.");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var order = await _removeOrderItem.ExecuteAsync(input.OrderId, input.Sku, input.Quantity, cancellationToken);
            SetFlash("success", $"Updated table {order.TableNumber}. {input.Quantity} x {input.Sku} removed.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SendToKitchen(Guid orderId, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _sendToKitchen.ExecuteAsync(orderId, cancellationToken);
            SetFlash("success", $"Table {order.TableNumber} has been fired to the kitchen.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay(PayOrderInputModel input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            SetFlash("error", "Select a payment method before charging the order.");
            return RedirectToAction(nameof(Index));
        }

        try
        {
            var order = await _processPayment.ExecuteAsync(input.OrderId, input.Method, cancellationToken);
            SetFlash("success", $"Payment approved for table {order.TableNumber}.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Close(Guid orderId, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _closeOrder.ExecuteAsync(orderId, cancellationToken);
            SetFlash("success", $"Closed ticket {ShortId(order.Id)} for table {order.TableNumber}.");
        }
        catch (Exception exception)
        {
            SetFlash("error", exception.Message);
        }

        return RedirectToAction(nameof(Index));
    }

    private void SetFlash(string tone, string message)
    {
        TempData["FlashTone"] = tone;
        TempData["FlashMessage"] = message;
    }

    private static string ShortId(Guid id) => id.ToString("N")[..8].ToUpperInvariant();
}
