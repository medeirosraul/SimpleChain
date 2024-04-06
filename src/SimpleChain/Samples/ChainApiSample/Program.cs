using ChainApiSample.Models.ProductTax;
using ChainApiSample.Models.TransactionSplit;
using ChainApiSample.Services.ProductTax;
using ChainApiSample.Services.TransactionSplit;
using SimpleChain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add tax chain.
builder.Services.AddChainFor<ProductModel>(options =>
{
    options.AddHandler<StateTaxService>();
    options.AddHandler<MunicipalTaxService>();
});

// Add transaction chain.
builder.Services.AddChainFor<Transaction>(options =>
{
    options.AddHandler<TransactionStateFeeHandler>();
    options.AddHandler<TransactionMunicipalFeeHandler>();
    options.AddHandler<TransactionSplitHandler>();
});

builder.Services.AddChainFor<Split>(options =>
{
    options.AddHandler<TransactionSplitFeeHandler>();
});

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
