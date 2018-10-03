using BlazorRedux;
using Claimini.BlazorClient.ApplicationState;

namespace Claimini.BlazorClient.Components
{
    public class BaseComponent : ReduxComponent<AppState, IAction>
    {
    }
}
