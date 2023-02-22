public class BowlPattern : ItemPattern
{
    public BowlPattern(int colorIndex, int step)
    {
        ColorIndex = colorIndex;
        StepPosition = step;
    }

    public override void ChangeColorIndexes(int newColorIndex)
    {
       ColorIndex = newColorIndex;
    }

    public  void SetStepPosition(int Step)
    {
        StepPosition = Step;
    }
}
 