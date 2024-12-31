using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EducationWebApp.Models
{
    // Remove unnecessary spaces - Completed
    // Add summary to the properties - Completed
    // Rename the class to RequestModel - Completed
    // Instead of creating seperate class library you can create the folder Models and create the model - I'm using the same model in both the projects
    public class RequestModel
    {
        /// <summary>
        /// Subject Type to Generate Quiz Question
        /// </summary>
        [Required] public string Subject { get; set; } = string.Empty;
        /// <summary>
        /// Difficulty Level to Generarating Quiz Question
        /// </summary>
        [Required] public string Difficulty { get; set; } = string.Empty;
        /// <summary>
        /// Number of Questions to Generatr
        /// </summary>
        [Range(1, 25)] public int Number { get; set; }
    }
}
