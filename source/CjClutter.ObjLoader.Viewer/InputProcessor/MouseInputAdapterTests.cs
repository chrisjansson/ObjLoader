using System.Windows.Forms;
using NUnit.Framework;
using FluentAssertions;
using Rhino.Mocks;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    [TestFixture]
    public class MouseInputAdapterTests
    {
        private MouseInputAdapter _mouseInputAdapter;
        private Control _sourceControl;

        private IMouseInputTarget _mouseInputTarget;

        [SetUp]
        public void SetUp()
        {
            _sourceControl = MockRepository.GenerateStub<Control>();
            _mouseInputTarget = MockRepository.GenerateStub<IMouseInputTarget>();

            _mouseInputAdapter = new MouseInputAdapter();
            _mouseInputAdapter.Target = _mouseInputTarget;
        }

        [Test]
        public void Unsubscribes_from_event_source_when_event_source_is_changed()
        {
            _mouseInputAdapter.Source = _sourceControl;
            _mouseInputAdapter.Source = null;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());

            _mouseInputTarget.AssertWasNotCalled(x => x.OnMouseMove(Arg<MouseInputEvent>.Is.Anything));
        }

        [Test]
        public void Subscribes_to_event_source_when_event_source_is_changed()
        {
            _mouseInputAdapter.Source = _sourceControl;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());

            _mouseInputTarget.AssertWasCalled(x => x.OnMouseMove(Arg<MouseInputEvent>.Is.Anything));
        }

        [Test]
        public void Raises_mouse_move_with_correct_parameters_on_mouse_move_event_in_control()
        {
            _mouseInputAdapter.Source = _sourceControl;

            const int expectedX = 10;
            const int expectedY = 20;
            var expectedEvent = new MouseEventArgs(MouseButtons.None, 0, expectedX, expectedY, 0);

            RaiseMouseMoveEvent(expectedEvent);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnMouseMove(new MouseInputEvent())).First();
            var actualEventArgs = (MouseInputEvent)arguments[0];
            var actualPosition = actualEventArgs.Position;

            actualPosition.X.Should().BeApproximately(expectedX);
            actualPosition.Y.Should().BeApproximately(expectedY);
        }

        private void RaiseMouseMoveEvent(MouseEventArgs arguments)
        {
            _sourceControl.Raise(x => x.MouseMove += null, _sourceControl, arguments);
        }
    }

    public static class MouseEventArgsExtensions
    {
        public static MouseEventArgs Empty()
        {
            return new MouseEventArgs(0, 0, 0, 0, 0);
        }
    }
    
    public class MouseInputAdapterSpy
    {
        public MouseInputEvent CapturedMouseMoveArguments { get; private set; }

        public virtual void OnMouseMove(MouseInputEvent eventArgs)
        {
            CapturedMouseMoveArguments = eventArgs;
        }
    }
}