using System;

namespace BoardGame
{
    // Composite design pattern
    // abstract class Graphic for Component
    public abstract class Graphic
    {
        public Seed content;
        public abstract void Draw();
    }
}
