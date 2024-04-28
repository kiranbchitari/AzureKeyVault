using System.Security.Cryptography.X509Certificates;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);


using var x509Store = new X509Store(StoreLocation.CurrentUser);

x509Store.Open(OpenFlags.ReadOnly);

var x509Certificate = x509Store.Certificates
    .Find(
        X509FindType.FindByThumbprint,
        "42C12875022FEDB2B5F5F3F6EA17F83514AA53F5",     //AzureADCertThumbprint
        validOnly: false)
    .OfType<X509Certificate2>()
    .Single();

builder.Configuration.AddAzureKeyVault(
    new Uri($"https://varad.vault.azure.net/"),    //KeyVaultName
    new ClientCertificateCredential(
        "82676786-5bc7-43c6-b8f8-b3ee02b0b5f3",  //AzureADDirectoryId
        "b8b44a68-2c26-4987-b6b9-21d115a2e83a",   //AzureADApplicationId
        x509Certificate));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
