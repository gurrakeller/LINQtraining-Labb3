using System;
using System.Collections.Generic;

namespace LINQtraining.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int Grade1 { get; set; } //This is the grade value 1-5 e.g the old grading system. in the database i set is as simply "grade" but for some reason vs didnt like that so it set it as grade1 instead. 
    //realize now that i could have set it to something like gradevalue instead of grade but oh well.

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly GradeDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
