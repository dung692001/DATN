using DATN.BLL.Interfaces;
using DATN.BLL.Services;
using DATN.DAL.Interfaces;
using DATN.DAL.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IBonusDisciplineRepository, BonusDisciplineRepository>();
builder.Services.AddScoped<IBonusDisciplineService, BonusDisciplineService>();

builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractService, ContractService>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEducationService, EducationService>();

builder.Services.AddScoped<IInsuranceRepository, InsuranceRepository>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();

builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();

builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IParentService, ParentService>();

builder.Services.AddScoped<IPositionsRepository, PositionsRepository>();
builder.Services.AddScoped<IPositionsService, PositionsService>();

builder.Services.AddScoped<ITimesheetsRepository, TimesheetsRepository>();
builder.Services.AddScoped<ITimesheetsService, TimesheetsService>();

builder.Services.AddScoped<ITimesheetsDetailRepository, TimesheetsDetailRepository>();
builder.Services.AddScoped<ITimesheetsDetailService, TimesheetsDetailService>();

builder.Services.AddScoped<IWorkingShiftRepository, WorkingShiftRepository>();
builder.Services.AddScoped<IWorkingShiftService, WorkingShiftService>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddControllers().AddNewtonsoftJson(Option =>
{
    Option.UseMemberCasing();
});
builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    {
        b.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader().Build();
    }
    );
}
);
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
