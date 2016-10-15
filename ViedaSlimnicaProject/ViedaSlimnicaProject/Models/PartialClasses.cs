using System;
using System.ComponentModel.DataAnnotations;

namespace ContosoSite.Models
{
    [MetadataType(typeof(Palata))]
    public partial class Student
    {
    }

    [MetadataType(typeof(Pacients))]
    public partial class Enrollment
    {
    }
}