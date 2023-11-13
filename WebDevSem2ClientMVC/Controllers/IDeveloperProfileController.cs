using Microsoft.AspNetCore.Mvc;
using WebDevSem2ClientMVC.Models;

namespace WebDevSem2ClientMVC.Controllers
{
    public interface IDeveloperProfileController
    {
        IActionResult Create();
        Task<IActionResult> Create([Bind(new[] { "DeveloperProfileId,Name,Skills,Discription,PictureURL,Email" })] DeveloperProfile developerProfile);
        Task<IActionResult> Delete(int? id);
        Task<IActionResult> DeleteConfirmed(int id);
        Task<IActionResult> Details(int? id);
        Task<IActionResult> Edit(int id, [Bind(new[] { "DeveloperProfileId,Name,Skills,Discription,PictureURL,Email" })] DeveloperProfile developerProfile);
        Task<IActionResult> Edit(int? id);
        Task<IActionResult> Index();
    }
}