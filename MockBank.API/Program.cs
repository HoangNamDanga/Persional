using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// 1. ??ng ký d?ch v?
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // H?t l?i sau khi ch?y l?nh ? B??c 1

var app = builder.Build();

// 2. C?u hình Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // H?t l?i
    app.UseSwaggerUI(); // H?t l?i
}

// 3. ??nh ngh?a Endpoint thanh toán
app.MapPost("/api/mockbank/process", ([FromBody] BankRequest request, IConfiguration config, IHttpClientFactory clientFactory) =>
{
    var callbackUrl = config["CallbackSettings:Url"];

    if (string.IsNullOrEmpty(callbackUrl))
    {
        return Results.BadRequest("Ch?a c?u hình Callback URL trong bi?n môi tr??ng.");
    }

    _ = Task.Run(async () =>
    {
        Console.WriteLine($"[MockBank] ?ang x? lý giao d?ch {request.PaymentId}...");
        await Task.Delay(5000);

        var client = clientFactory.CreateClient();
        var result = new BankResponse(
            request.PaymentId,
            "Success",
            "MOCK-BANK-" + Guid.NewGuid().ToString().ToUpper()[..8]
        );

        try
        {
            await client.PostAsJsonAsync(callbackUrl, result);
            Console.WriteLine($"[MockBank] Callback thành công cho: {request.PaymentId}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MockBank] L?i Callback: {ex.Message}");
        }
    });

    return Results.Ok(new { message = "Giao d?ch ?ang x? lý...", paymentId = request.PaymentId });
});

app.Run();

// 4. ??nh ngh?a Model (PH?I ?? ? D??I CÙNG FILE)
public record BankRequest(decimal PaymentId, decimal Amount);
public record BankResponse(decimal PaymentId, string Status, string TransactionId);