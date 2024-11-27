using Temp.Components.Mvc;

namespace Temp.Objects;

public class ProfileDeleteView : AView
{
    [NotTrimmed]
    [StringLength(32)]
    public String Password { get; set; }
}
