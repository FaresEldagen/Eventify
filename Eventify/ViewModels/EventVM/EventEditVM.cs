using Eventify.Models.Entities;
using Eventify.Models.Enums;
using Eventify.Validators;
using System.ComponentModel.DataAnnotations;

namespace Eventify.ViewModels.EventVM
{
    public class EventEditVM : EventAddVM
    {
        //public int OriginalPhotoCount { get; set; }
        //[EnsureAtLeastOnePhoto("OriginalPhotoCount", "DeletedPhotos")]
        //[MinPhotos(1)]
        //public List<IFormFile> FormFiles { get; set; } = new List<IFormFile>();

        public List<string> DeletedPhotos { get; set; } = new List<string>();
    }
}
