
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PTMA.DB;
using PTMA.Models;
using PTMA_API.Model;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<PtmaDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PtmaDBContext")));

builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<PtmaDBContext>();

//add config authentcation to API

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])),
    };
});

builder.Services.AddAuthorization();



//for versionApi 
builder.Services.AddEndpointsApiExplorer();//build in asp.net service view api enformation like endpoint and how contract wheth him

builder.Services.AddApiVersioning(setup =>
{
    setup.ReportApiVersions = true;
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.ApiVersionReader = ApiVersionReader.Combine(
        //  new QueryStringApiVersionReader("api-version"),
        // new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));//This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
                                              //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.

}).AddApiExplorer(setup =>
{
    setup.SubstituteApiVersionInUrl = true;// Add versioned API explorer

});


var apiVersionDiscreptionProvider = builder.Services.BuildServiceProvider()
    .GetRequiredService<IApiVersionDescriptionProvider>();

//add  Authoraization for swagger

builder.Services.AddSwaggerGen(options =>
{
    //for dicumntation api

    foreach (var discrption in apiVersionDiscreptionProvider.ApiVersionDescriptions)
    {
        options.SwaggerDoc(
            $"{discrption.GroupName}",
            new()
            {
                Title = "Library",
                Version = discrption.ApiVersion.ToString(),
                Description = "this Api use for Book Library"
            }
            );

    }
    var xmlCommitFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlFullCommitPath = Path.Combine(AppContext.BaseDirectory, xmlCommitFile);
    options.IncludeXmlComments(xmlFullCommitPath);
    /////
    ///
    ///for authorization

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Key"
    });
    //to add token in header request
    options.AddSecurityRequirement(new OpenApiSecurityRequirement(){
        {
        new OpenApiSecurityScheme()
        {
            Reference=new OpenApiReference()
            {
                Type=ReferenceType.SecurityScheme,
                Id="Bearer",

            },
            Name="Bearer",
            In=ParameterLocation.Header,

        },
         new List<string>()


    }
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        var documants = app.DescribeApiVersions();
        foreach (var documant in documants)
        {
            setup.SwaggerEndpoint($"/swagger/{documant.GroupName}/swagger.json",
                documant.GroupName.ToUpperInvariant()
                );
        }
    });
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

DbInitializer.Initialize(app);
app.Run();
