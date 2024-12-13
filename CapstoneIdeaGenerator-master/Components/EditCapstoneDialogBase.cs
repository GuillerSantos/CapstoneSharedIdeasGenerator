using CapstoneIdeaGenerator.Client.Models.DTOs;
using CapstoneIdeaGenerator.Client.Services.Contracts;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CapstoneIdeaGenerator.Client.Components
{
    public class EditCapstoneDialogBase : ComponentBase
    {
        [Parameter] public CapstonesDTO capstones { get; set; } = new CapstonesDTO();

        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }
        [Inject] private ICapstoneService CapstoneService { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }

        public async Task Save()
        {
            if (capstones != null)
            {
                if (capstones.CapstoneId == 0)
                {
                    await CapstoneService.AddCapstone(capstones);
                }
                else
                {
                    await CapstoneService.UpdateCapstone(capstones.CapstoneId, capstones);
                }
            }
            else
            {
                Snackbar.Add("Error: Capstone Not Found", Severity.Error);
            }

            MudDialog.Close(DialogResult.Ok(true));
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}