using System.Windows.Forms;
using CjClutter.ObjLoader.Viewer.Extensions;
using NUnit.Framework;
using FluentAssertions;
using OpenTK;
using Rhino.Mocks;
using System.Linq;

namespace CjClutter.ObjLoader.Viewer.InputAdapters
{
    [TestFixture]
    public class MouseInputAdapterTests
    {
        
        private MouseInputAdapter _mouseInputAdapter;
        private Control _sourceControl;

        private IMouseInputTarget _mouseInputTarget;

        private const int ExpectedX = 10;
        private const int ExpectedY = 20;
        private const int MouseMoveX = 20;
        private const int MouseMoveY = 22;
        private const int MouseUpX = 30;
        private const int MouseUpY = 40;
        private MouseEventArgs _expectedMouseMoveEvent;
        private MouseEventArgs _expectedMouseDownEvent;
        private MouseEventArgs _mouseMoveArguments;
        private MouseEventArgs _expectedMouseUpEvent;

        [SetUp]
        public void SetUp()
        {
            _sourceControl = MockRepository.GenerateStub<Control>();
            _mouseInputTarget = MockRepository.GenerateStub<IMouseInputTarget>();

            _mouseInputAdapter = new MouseInputAdapter();
            _mouseInputAdapter.Target = _mouseInputTarget;

            _expectedMouseMoveEvent = new MouseEventArgs(MouseButtons.None, 0, ExpectedX, ExpectedY, 0);
            _expectedMouseDownEvent = new MouseEventArgs(MouseButtons.Left, 0, ExpectedX, ExpectedY, 0);
            _expectedMouseUpEvent = new MouseEventArgs(MouseButtons.Left, 0, MouseUpX, MouseUpY, 0);
            _mouseMoveArguments = new MouseEventArgs(MouseButtons.None, 0, MouseMoveX, MouseMoveY, 0);

            _mouseInputAdapter.Source = _sourceControl;
        }

        [Test]
        public void Unsubscribes_from_event_source_when_event_source_is_changed()
        {
            _mouseInputAdapter.Source = null;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());
            RaiseMouseDownEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));
            RaiseMouseUpEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));

            _mouseInputTarget.AssertWasNotCalled(x => x.OnMouseMove(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasNotCalled(x => x.OnLeftMouseButtonDown(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasNotCalled(x => x.OnLeftMouseButtonUp(Arg<Vector2d>.Is.Anything));
        }

        [Test]
        public void Subscribes_to_event_source_when_event_source_is_changed()
        {
            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());
            RaiseMouseDownEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));
            RaiseMouseUpEvent(MouseEventArgsExtensions.WithButtons(MouseButtons.Left));

            _mouseInputTarget.AssertWasCalled(x => x.OnMouseMove(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasCalled(x => x.OnLeftMouseButtonDown(Arg<Vector2d>.Is.Anything));
            _mouseInputTarget.AssertWasCalled(x => x.OnLeftMouseButtonUp(Arg<Vector2d>.Is.Anything));
        }

        [Test]
        public void Raises_mouse_move_with_correct_parameters_on_mouse_move_event_in_control()
        {
            RaiseMouseMoveEvent(_expectedMouseMoveEvent);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnMouseMove(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            actualPosition.X.Should().BeApproximately(ExpectedX);
            actualPosition.Y.Should().BeApproximately(ExpectedY);
        }

        [Test]
        public void Raises_left_mouse_button_down_with_correct_parameters_on_mouse_down_event_in_control()
        {
            RaiseMouseDownEvent(_expectedMouseDownEvent);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnLeftMouseButtonDown(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            actualPosition.X.Should().BeApproximately(ExpectedX);
            actualPosition.Y.Should().BeApproximately(ExpectedY);
        }

        [Test]
        public void Raises_left_mouse_button_up_with_correct_parameters_on_mouse_up_event_in_control()
        {
            RaiseMouseUpEvent(_expectedMouseDownEvent);

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnLeftMouseButtonUp(new Vector2d())).First();
            var actualPosition = (Vector2d)arguments[0];

            _mouseInputTarget.AssertWasCalled(x => x.OnLeftMouseButtonUp(new Vector2d()), x => x.IgnoreArguments());
            actualPosition.X.Should().BeApproximately(ExpectedX);
            actualPosition.Y.Should().BeApproximately(ExpectedY);
        }

        [Test]
        public void Raises_mouse_drag_with_correct_parameters_on_mouse_drag()
        {
            RaiseMouseDownEvent(_expectedMouseDownEvent);
            RaiseMouseMoveEvent(_mouseMoveArguments);

            _mouseInputTarget.AssertWasCalled(x => x.OnMouseDrag(new MouseDragEventArgs()), x=> x.IgnoreArguments());
            
            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnMouseDrag(new MouseDragEventArgs())).First();
            var actualMouseDragEventArgs = (MouseDragEventArgs)arguments[0];

            var actualStartPosition = actualMouseDragEventArgs.StartPosition;
            var actualEndPosition = actualMouseDragEventArgs.EndPosition;

            actualStartPosition.X.Should().BeApproximately(ExpectedX);
            actualStartPosition.Y.Should().BeApproximately(ExpectedY);
            actualEndPosition.X.Should().BeApproximately(MouseMoveX);
            actualEndPosition.Y.Should().BeApproximately(MouseMoveY);
        }

        [Test]
        public void Does_not_raise_mouse_drag_after_mouse_up()
        {
            RaiseMouseDownEvent(_expectedMouseDownEvent);
            RaiseMouseUpEvent(_expectedMouseDownEvent);

            _mouseInputTarget.Expect(x => x.OnMouseDrag(new MouseDragEventArgs()))
                .IgnoreArguments()
                .Repeat.Never();

            RaiseMouseMoveEvent(_expectedMouseMoveEvent);

            _mouseInputTarget.VerifyAllExpectations();
        }

        [Test]
        public void Does_not_raise_mouse_drag_before_mouse_down()
        {
            _mouseInputTarget.Expect(x => x.OnMouseDrag(new MouseDragEventArgs()))
                .IgnoreArguments()
                .Repeat.Never();

            RaiseMouseMoveEvent(_expectedMouseMoveEvent);

            _mouseInputTarget.VerifyAllExpectations();
        }

        [Test]
        public void Raises_mouse_drag_end_on_mouse_up()
        {
            RaiseMouseDownEvent(_expectedMouseDownEvent);
            RaiseMouseUpEvent(_expectedMouseUpEvent);

            _mouseInputTarget.AssertWasCalled(x => x.OnMouseDragEnd(new MouseDragEventArgs()), x => x.IgnoreArguments());

            var arguments = _mouseInputTarget.GetArgumentsForCallsMadeOn(x => x.OnMouseDragEnd(new MouseDragEventArgs())).First();
            var actualMouseDragEventArgs = (MouseDragEventArgs)arguments[0];
            var startPosition = actualMouseDragEventArgs.StartPosition;
            var endPosition = actualMouseDragEventArgs.EndPosition;

            startPosition.X.Should().BeApproximately(ExpectedX);
            startPosition.Y.Should().BeApproximately(ExpectedY);
            endPosition.X.Should().BeApproximately(MouseUpX);
            endPosition.Y.Should().BeApproximately(MouseUpY);
        }

        [Test]
        public void Does_not_raise_mouse_drag_end_on_mouse_up_before_mouse_down()
        {
            _mouseInputTarget.Expect(x => x.OnMouseDragEnd(new MouseDragEventArgs()))
                .IgnoreArguments()
                .Repeat.Never();
            
            RaiseMouseUpEvent(_expectedMouseUpEvent);

            _mouseInputTarget.VerifyAllExpectations();
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
}