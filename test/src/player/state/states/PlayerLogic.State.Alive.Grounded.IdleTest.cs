namespace GameDemo.Tests;

using Chickensoft.GoDotTest;
using Chickensoft.LogicBlocks;
using Godot;
using Shouldly;

public class PlayerLogicStateAliveGroundedIdleTest : TestClass {
  private IFakeContext _context = default!;
  private PlayerLogic.State.Idle _state = default!;

  public PlayerLogicStateAliveGroundedIdleTest(Node testScene) :
    base(testScene) { }

  [Setup]
  public void Setup() {
    _state = new();
    _context = _state.CreateFakeContext();
  }

  [Test]
  public void Enters() {
    var parent =
      new PlayerLogic.State.Grounded();
    _state.Enter(parent);

    _context.Outputs.ShouldBe(new object[] {
      new PlayerLogic.Output.Animations.Idle()
    });
  }

  [Test]
  public void OnStartedMovingHorizontallyGoesToMoving() {
    var next = _state.On(new PlayerLogic.Input.StartedMovingHorizontally());

    next.ShouldBeAssignableTo<PlayerLogic.State.Moving>();
  }
}
