using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Models;
using URL_Shortener.Client.Models.DTOs.ShortUrl;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Controllers;

public class AboutController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public AboutController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // GET
    public IActionResult Index()
    {
        try
        {
            var urlShortAlgorithm = _unitOfWork.UrlShortenedDbContext.UrlShortAlgorithms.FirstOrDefault();
        
            return View(urlShortAlgorithm);
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(){Message = e.Message});
        }
    }
    
    [Authorize("Admin")]
    public IActionResult Edit(UrlShortenerAlgorithmDto shortUrlAlgorithm)
    {
        try
        {
            _unitOfWork.UrlShortenedDbContext.Update(new UrlShortenerAlgorithm
            {
                Id = shortUrlAlgorithm.Id,
                Description = shortUrlAlgorithm.Description
            });
            _unitOfWork.SaveChanges();
            
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel(){Message = e.Message});
        }
    }
}