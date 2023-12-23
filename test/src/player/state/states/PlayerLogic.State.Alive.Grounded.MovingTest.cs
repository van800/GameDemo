namespace GameDemo.Tests;

using Chickensoft.GoDotTest;
using Chickensoft.LogicBlocks;
using Godot;
using Moq;
using Shouldly;

public class PlayerLogicStateAliveGroundedMovingTest : TestClass {
  private IFakeContext _context = default!;
  private Mock<IAppRepo> _appRepo = default!;
  private PlayerLogic.State.Moving _state = default!;

  public PlayerLogicStateAliveGroundedMovingTest(Node testScene) :
    base(testScene) { }

  [Setup]
  public void Setup() {
    _appRepo = new();
    _state = new();
    _context = _state.CreateFakeContext();

    _context.Set(_appRepo.Object);
  }

  [Test]
  public void Enters() {
    var parent = new PlayerLogic.State.Grounded();
    _state.Enter(parent);

    _context.Outputs.ShouldBe(new object[] {
      new PlayerLogic.Output.Animations.Move()
    });
  }

  [Test]
  public void OnStoppedMovingHorizontallyIdles() {
    var next = _state.On(new PlayerLogic.Input.StoppedMovingHorizontally());

    next.ShouldBeAssignableTo<PlayerLogic.State.Idle>();
  }
}
