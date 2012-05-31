using System.Windows.Forms;
using NUnit.Framework;
using FluentAssertions;
using OpenTK;
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
            RaiseMouseDownEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));

            _mouseInputTarget.AssertWasNotCalled(x => x.OnMouseMove(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasNotCalled(x => x.OnLeftMouseButtonDown(Arg<Vector2d>.Is.Anything));
        }

        [Test]
        public void Subscribes_to_event_source_when_event_source_is_changed()
        {
            _mouseInputAdapter.Source = _sourceControl;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());
            RaiseMouseDownEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));

            _mouseInputTarget.AssertWasCalled(x => x.OnMouseMove(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasCalled(x => x.OnLeftMouseButtonDown(Arg<Vector2d>.Is.Anything));
        }

        [Test]
        public void Raises_mouse_move_with_correct_parameters_on_mouse_move_event_in_control()
        {
            _mouseInputAdapter.Source = _sourceControl;

            const int expectedX = 10;
            const int expectedY = 20;
            var expectedEvent = new MouseEventArgs(MouseButtons.None, 0, expectedX, expectedY, 0);

            RaiseMouseMoveEvent(expectedEvent);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnMouseMove(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            actualPosition.X.Should().BeApproximately(expectedX);
            actualPosition.Y.Should().BeApproximately(expectedY);
        }

        [Test]
        public void Raises_left_mouse_button_down_with_correct_parameters_on_mouse_down_event_in_control()
        {
            _mouseInputAdapter.Source = _sourceControl;

            const int expectedX = 10;
            const int expectedY = 20;
            var expectedArguments = new MouseEventArgs(MouseButtons.Left, 0, expectedX, expectedY, 0);

            RaiseMouseDownEvent(expectedArguments);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnLeftMouseButtonDown(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            actualPosition.X.Should().BeApproximately(expectedX);
            actualPosition.Y.Should().BeApproximately(expectedY);
        }

        [Test]
        public void Raises_left_mouse_button_up_with_correct_parameters_on_mouse_up_event_in_control()
        {
            _mouseInputAdapter.Source = _sourceControl;

            const int expectedX = 30;
            const int expectedY = 40;
            var expectedArguments = new MouseEventArgs(MouseButtons.Left, 0, expectedX, expectedY, 0);

            RaiseMouseUpEvent(expectedArguments);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnLeftMouseButtonUp(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            actualPosition.X.Should().BeApproximately(expectedX);
            actualPosition.Y.Should().BeApproximately(expectedY);
        }

        private void RaiseMouseMoveEvent(MouseEventArgs arguments)
        {
            _sourceControl.Raise(x => x.MouseMove += null, _sourceControl, arguments);
        }

        private void RaiseMouseDownEvent(MouseEventArgs arguments)
        {
            _sourceControl.Raise(x => x.MouseDown += null, _sourceControl, arguments);
        }

        private void RaiseMouseUpEvent(MouseEventArgs arguments)
        {
            _sourceControl.Raise(x => x.MouseUp += null, _sourceControl, arguments);
        }
    }

    public static class MouseEventArgsExtensions
    {
        public static MouseEventArgs Empty()
        {
            return new MouseEventArgs(0, 0, 0, 0, 0);
        }

        public static MouseEventArgs WithButtons(MouseButtons buttons)
        {
            return new MouseEventArgs(buttons, 0, 0, 0, 0);
        }
    }
    
    public class MouseInputAdapterSpy
    {
        public MouseMoveInputEventArguments CapturedMouseMoveMoveArguments { get; private set; }

        public virtual void OnMouseMove(MouseMoveInputEventArguments eventArgumentsArgs)
        {
            CapturedMouseMoveMoveArguments = eventArgumentsArgs;
        }
    }
}