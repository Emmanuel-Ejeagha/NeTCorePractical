using System;
using Microsoft.AspNetCore.Mvc;
using XkcdComicFinder;

namespace ComicFindService.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController
{
    private readonly ComicFinder _comicFinder;
    public SearchController(ComicFinder comicFinder)
      => _comicFinder = comicFinder;

    [HttpGet]
    public Task<IAsyncEnumerable<Comic>> FindAsync(string searchText) =>
        _comicFinder.FindAsync(searchText);
}
