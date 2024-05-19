public class Entity
{
    Simulator simulator;
    public void SetSimulator(Simulator value) { simulator = value; }

    public void FixedUpdate()
    {
        simulator.RandomNext();
    }
}