using Temp.Components.Tree;

namespace Temp.Objects;

public class RoleViewTests
{
    [Fact]
    public void RoleView_CreatesEmpty()
    {
        MvcTree actual = new RoleView().Permissions;

        Assert.Empty(actual.SelectedIds);
        Assert.Empty(actual.Nodes);
    }
}
