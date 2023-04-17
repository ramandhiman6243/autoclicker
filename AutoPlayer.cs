public class AutoPlayer
{
    Configuration.DataContainer config;

    int currentWait = 0;

    int currentStep = 0;

    public AutoPlayer(Configuration.DataContainer config)
    {
        this.config = config;
    }

    public void Reset()
    {
        SoftReset();
    }

    void SoftReset()
    {
        currentWait = config.steps[0].preDelay;
        currentStep = 0;
    }

    static void MoveCursor(int x, int y)
    {
        MouseUtils.POINT p = new MouseUtils.POINT(x, y);
        MouseUtils.SetCursorPos(p.x, p.y);
    }

    public void Play(int delta)
    {
        if (config == null)
            return;

        if (config.steps == null)
            return;

        if (currentStep < config.steps.Length)
        {
            var step = config.steps[currentStep];

            if (currentWait > 0)
            {
                currentWait -= delta;
            }
            else
            {
                currentWait = step.preDelay;

                switch (step.type)
                {
                    case "move":
                        MoveCursor(step.x, step.y);
                        break;

                    case "click":
                        MoveCursor(step.x, step.y);
                        MouseUtils.MouseEvent(MouseUtils.MouseEventFlags.LeftDown);
                        MouseUtils.MouseEvent(MouseUtils.MouseEventFlags.LeftUp);
                        break;
                }

                currentStep++;
            }
        }
        else
        {
            SoftReset();
        }
    }
}