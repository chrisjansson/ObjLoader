using System.Windows.Forms;
using NUnit.Framework;
using FluentAssertions;
using Rhino.Mocks;

namespace CjClutter.ObjLoader.Viewer.InputProcessor
{
    [TestFixture]
    public class MouseInputProcessorTests
    {
        private MouseInputProcessor _mouseInputProcessor;
        private Control _control;
        private MouseInputProcessorEventSpy _mouseInputProcessorEventSpy;
        private MouseInputProcessorEventSpy _mouseInputProcessorEventSpyMock;

        [SetUp]
        public void SetUp()
        {
            _control = MockRepository.GenerateStub<Control>();
            _mouseInputProcessorEventSpy = new MouseInputProcessorEventSpy();
            _mouseInputProcessorEventSpyMock = MockRepository.GenerateMock<MouseInputProcessorEventSpy>();

            _mouseInputProcessor = new MouseInputProcessor();

            SetupEventSpy();
            SetupEventSpyMock();
        }

        public void SetupEventSpy()
        {
            _mouseInputProcessor.MouseMove += _mouseInputProcessorEventSpy.OnMouseMove;
        }

        private void SetupEventSpyMock()
        {
            _mouseInputProcessor.MouseMove += _mouseInputProcessorEventSpyMock.OnMouseMove;
        }

        [Test]
        public void Unsubscribes_from_event_source_when_event_source_is_changed()
        {
            _mouseInputProcessor.Source = _control;
            _mouseInputProcessor.Source = null;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());

            _mouseInputProcessorEventSpyMock.AssertWasNotCalled(x => x.OnMouseMove(Arg<MouseInputEvent>.Is.Anything));
        }

        [Test]
        public void Subscribes_to_event_source_when_event_source_is_changed()
        {
            _mouseInputProcessor.Source = _control;

            RaiseMouseMoveEvent(MouseEventArgsExtensions.Empty());

            _mouseInputProcessorEventSpyMock.AssertWasCalled(x => x.OnMouseMove(Arg<MouseInputEvent>.Is.Anything));
        }

        [Test]
        public void Raises_mouse_move_with_correct_parameters_on_mouse_move_event_in_control()
        {
            _mouseInputProcessor.Source = _control;

            const int expectedX = 10;
            const int expectedY = 20;
            var expectedEvent = new MouseEventArgs(MouseButtons.None, 0, expectedX, expectedY, 0);

            RaiseMouseMoveEvent(expectedEvent);

            var actualEventArgs = _mouseInputProcessorEventSpy.CapturedMouseMoveArguments;
            var actualPosition = actualEventArgs.Position;

            actualPosition.X.Should().BeApproximately(expectedX);
            actualPosition.Y.Should().BeApproximately(expectedY);
        }

        private void RaiseMouseMoveEvent(MouseEventArgs arguments)
        {
            _control.Raise(x => x.MouseMove += null, _control, arguments);
        }
    }

    public static class MouseEventArgsExtensions
    {
        public static MouseEventArgs Empty()
        {
            return new MouseEventArgs(0, 0, 0, 0, 0);
        }
    }
    
    public class MouseInputProcessorEventSpy
    {
        public MouseInputEvent CapturedMouseMoveArguments { get; private set; }

        public virtual void OnMouseMove(MouseInputEvent eventArgs)
        {
            CapturedMouseMoveArguments = eventArgs;
        }
    }
}