using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using URL_Shortener.Client.Interfaces.ShortUrl;
using URL_Shortener.Client.Interfaces.UnitOfWork;
using URL_Shortener.Client.Models;
using URL_Shortener.Client.Models.DTOs.ShortUrl;
using URL_Shortener.Client.Models.Entities;

namespace URL_Shortener.Client.Controllers;

public class ShortUrlController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IShortUrlService _shortUrlService;

    public ShortUrlController(IUnitOfWork unitOfWork, IShortUrlService shortUrlService)
    {
        _unitOfWork = unitOfWork;
        _shortUrlService = shortUrlService;
    }

    public IActionResult Index()
    {
        var shortenedUrls = _unitOfWork.ShortenedUrlRepository.GetAll();
        return View(shortenedUrls);
    }
    
    [Authorize("User")]
    [HttpPost]
    public ActionResult<ShortUrlResponseDto> Add(string url)
    {
        try
        {
            var shortUrl = _shortUrlService.ShortUrl(url);
            
            var shortenedUrl = new ShortenedUrl()
            {
                OriginalUrl = url,
                ShortUrl = shortUrl,
                User = _unitOfWork.UserRepository.GetUserByLogin(Request.Cookies["X-User-Login"]),
                CreatedDate = DateTime.Now
            };
            _unitOfWork.ShortenedUrlRepository.Add(shortenedUrl);
            _unitOfWork.SaveChanges();
            
            var shortUrlResponse = new ShortUrlResponseDto()
            {
                Id = shortenedUrl.Id,
                OriginalUrl = url,
                ShortUrl = shortUrl
            };
            return Ok(shortUrlResponse);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Authorize("User")]
    [HttpDelete]
    public IActionResult Delete(int idUrl)
    {
        try
        {
            var userRole = Request.Cookies["X-User-Role"];

            if (userRole == "User")
            {
                var shortenedUrl = _unitOfWork.ShortenedUrlRepository.GetById(idUrl);
                if (shortenedUrl.User.Role.Name != userRole)
                {
                    return BadRequest("You don't have necessary rules to delete this url!");
                }
                
                _unitOfWork.ShortenedUrlRepository.Delete(idUrl);
                _unitOfWork.SaveChanges();
                
                return Ok();
            }
            
            _unitOfWork.ShortenedUrlRepository.Delete(idUrl);
            _unitOfWork.SaveChanges();
                
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    public IActionResult Info(string originalUrl)
    {
        try
        {
            var shortenedUrl = _unitOfWork.ShortenedUrlRepository.GetByOriginalUrl(originalUrl);
            if (shortenedUrl == null)
            {
                return View("Error", new ErrorViewModel() { Message = "The specified short url doesn't exist"});
            }
            
            return View(shortenedUrl);
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel() { Message = e.Message});
        }
    }
}