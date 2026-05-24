using DatabaseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Valuator.Pages;

[Authorize]
public class SummaryModel : PageModel
{
    private readonly ILogger<SummaryModel> _logger;
    private readonly IDatabaseService _db;

    public SummaryModel(ILogger<SummaryModel> logger, IDatabaseService db)
    {
        _logger = logger;
        _db = db;
    }

    public double Rank { get; set; }
    public bool IsRankCalculated { get; set; }
    public double Similarity { get; set; }

    public IActionResult OnGet(string id)
    {
        _logger.LogDebug(id);

        string shardKey = _db.Get("MAIN", id);
        string[] values = _db.Get(shardKey, [$"RANK-{id}", $"SIMILARITY-{id}", id]);

        if (User.Identity.Name != values[2])
        {
            return Redirect("index");
        }

        if (!string.IsNullOrWhiteSpace(values[0]) && double.TryParse(values[0], out double rank))
        {
            Rank = rank;
            IsRankCalculated = true;
        }
        else
        {
            IsRankCalculated = false;
        }

        if (double.TryParse(values[1], out double similarity))
        {
            Similarity = similarity;
        }

        return Page();
    }
}
