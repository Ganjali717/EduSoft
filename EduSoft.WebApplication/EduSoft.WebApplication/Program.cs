using EduSoft.Data.DatabaseContext;
using EduSoft.Data.Managers.Interfaces;
using EduSoft.Data.Managers.Services;
using EduSoft.Model.Automapper;
using Microsoft.EntityFrameworkCore;

#region Builder
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddAutoMapper(c => c.AddProfile<BaseAutoMapper>(), typeof(Program));
builder.Services.AddTransient<AppDbContext>();
builder.Services.AddScoped<ITutorialManager, TutorialManager>();
builder.Services.AddScoped<IChapterManager, ChapterManager>();
builder.Services.AddScoped<ISubChapterManager, SubChapterManager>();
builder.Services.AddScoped<IAccountManager, AccountManager>();
builder.Services.AddScoped<IJobManager, JobManager>();
builder.Logging.AddLog4Net("log4net.config");
#endregion


#region App
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion

public partial class Program
{

}