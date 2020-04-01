

namespace Microsoft.Dexterity.Bridge.Extended
{
    public interface IEventDescription
    {

        bool Registered { get; }

        short TagId { get; }

        void Register();

        void UnsubscribeAll();
    }
}
