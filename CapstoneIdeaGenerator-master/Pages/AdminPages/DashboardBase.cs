using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CapstoneIdeaGenerator.Client.Pages.AdminPages
{
    public class DashboardBase : ComponentBase
    {
        [Inject] IActivityLogsService activityLogsService { get; set; }
        [Inject] ISnackbar snackbar { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }
        [Inject] IActivityLogsService independentActivityLogsService { get; set; }

        public List<ActivityLogsDTO> activityLogs { get; private set; } = new List<ActivityLogsDTO>();
        public ActivityLogsDTO activityLogsDTO { get; set; } = new ActivityLogsDTO();
        public bool isLoading = false;

        public int Index = -1;
        public ChartOptions Options = new ChartOptions();


        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            try
            {
                var response = await activityLogsService.GetAllActivityLogs();
                if (response != null)
                {
                    await independentActivityLogsService.LogAdminAction("Viewed Dashboard");
                }
                activityLogs = response?.ToList() ?? new List<ActivityLogsDTO>();

                if (response == null)
                {
                    snackbar.Add("No Capstones Found", Severity.Warning);
                }
                else
                {
                    activityLogs = response.ToList();
                }

                isLoading = false;
            }
            catch (Exception ex)
            {
                snackbar.Add($"Exception Error: {ex.Message}", Severity.Error);
                navigationManager.NavigateTo("/home");
                isLoading = false;
            }

            StateHasChanged();
        }



        public MudDateRangePicker dateRangePickerRef;
        public PickerVariant variant = PickerVariant.Static;
        public DateRange dateRange = new(DateTime.Now.AddDays(-7), DateTime.Now);


        public List<ChartSeries> Series = new List<ChartSeries>()
        {
            new ChartSeries() { Name = "Generates", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
            new ChartSeries() { Name = "Ratings", Data = new double[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 } },
        };

        public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep" };

    }
}
