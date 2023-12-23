namespace GameDemo.Tests;

using Chickensoft.GoDotTest;
using Chickensoft.LogicBlocks;
using Godot;
using Moq;
using Shouldly;

public class PlayerLogicStateAliveGroundedTest : TestClass {
  private IFakeContext _context = default!;
  private Mock<IPlayer> _player = default!;
  private Mock<IAppRepo> _appRepo = default!;
  private PlayerLogic.Settings _settings = default!;
  private PlayerLogic.State.Grounded _state = default!;

  public PlayerLogicStateAliveGroundedTest(Node testScene) :
    base(testScene) { }

  [Setup]
  public void Setup() {
    _player = new Mock<IPlayer>();
    _appRepo = new Mock<IAppRepo>();
    _settings = new PlayerLogic.Settings(1, 1, 1, 1, 1, 1, 1);

    _state = new();
    _context = _state.CreateFakeContext();

    _context.Set(_player.Object);
    _context.Set(_appRepo.Object);
    _context.Set(_settings);
  }

  [Test]
  public void JumpGoesToJumping() {
    _player.Setup(player => player.Velocity).Returns(Vector3.Zero);

    var next = _state.On(new PlayerLogic.Input.Jump(1d));

    next.ShouldBeAssignableTo<PlayerLogic.State.Jumping>();

    _context.Outputs.ShouldBeOfTypes(new[] {
      typeof(PlayerLogic.Output.VelocityChanged)
    });
  }

  [Test]
  public void LeftFloorGoesToFallingOrLiftoff() {
    _state.On(new PlayerLogic.Input.LeftFloor(IsFalling: true))
      .ShouldBeAssignableTo<PlayerLogic.State.Falling>();

    _state.On(new PlayerLogic.Input.LeftFloor(IsFalling: false))
      .ShouldBeAssignableTo<PlayerLogic.State.Liftoff>();
  }
}
