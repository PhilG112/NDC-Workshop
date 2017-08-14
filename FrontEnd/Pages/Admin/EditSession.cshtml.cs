using ConferenceDTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

public class EditSessionModel : PageModel
{
    private readonly IApiClient _apiClient;

    public EditSessionModel(IApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [BindProperty]
    public Session Session { get; set; }

    [TempData]
    public string Message { get; set; }

    public bool ShowMessage => !string.IsNullOrEmpty(Message);

    public async Task OnGetAsync(int id)
    {
        var session = await _apiClient.GetSessionAsync(id);
        Session = new Session
        {
            ID = session.ID,
            ConferenceID = session.ConferenceID,
            TrackId = session.TrackId,
            Title = session.Title,
            Abstract = session.Abstract,
            StartTime = session.StartTime,
            EndTime = session.EndTime
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _apiClient.PutSessionAsync(Session);

        Message = "Session changes saved successfully.";
        
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var session = await _apiClient.GetSessionAsync(id);

        if (session != null)
        {
            await _apiClient.DeleteSessionAsync(id);
        }

        Message = "Session deleted successfully!";

        return RedirectToPage("/Index");
    }
}