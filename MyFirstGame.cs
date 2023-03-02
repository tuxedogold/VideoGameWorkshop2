using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MyFirstGame;

public class MyFirstGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private MainFish _mainFish;
    private Fish _otherFish;


    /*Instantiate nonsprite objects here*/
    public MyFirstGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.ToggleFullScreen();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    /*Instantiate your sprites here*/
    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var halfwidth = GraphicsDevice.Viewport.Bounds.Width / 2;
        var halfheight = GraphicsDevice.Viewport.Bounds.Height / 2;
        _mainFish = new MainFish(Content.Load<Texture2D>("img/FishLeft"),
                                                        new Vector2(halfwidth,halfheight), 
                                                        "left",
                                                        Content); 

        _otherFish = new Fish(Content.Load<Texture2D>("img/RedFish"),
                                                        new Vector2(10,50), //(X,Y). top left corner X goes left, Y goes down
                                                        GraphicsDevice.Viewport.Bounds.Width,
                                                        GraphicsDevice.Viewport.Bounds.Height
                                                        );

    }

    // input related logic
    protected override void Update(GameTime gameTime)
    {
        _mainFish.Update();
        _otherFish.Update();

        if(Keyboard.GetState().IsKeyDown(Keys.Escape)) // Escape key, exit game
        {
            System.Environment.Exit(0); // exit code zero indicats there was no programmatic error
        }

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    // game logic
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        _mainFish.Draw(_spriteBatch);
        _otherFish.Draw(_spriteBatch);
        if(IsInCollision(_mainFish,_otherFish))
        {
             System.Environment.Exit(0);
        }

        _spriteBatch.End();
        

        base.Draw(gameTime);
    }



    protected bool IsInCollision(Sprite a, Sprite b)
    {
        if (a.Position.Y + a.Height < b.Position.Y)
            return false;
        if (a.Position.Y > b.Position.Y + b.Height)
            return false;

        if (a.Position.X + a.Width < b.Position.X)
            return false;
        if (a.Position.X > + b.Position.X + b.Width)
            return false;
        return true;
    }
}

public abstract class Sprite
{
    public Texture2D Texture;
    public Vector2 Position;

    public int Width;
    public int Height;
    public Sprite(Texture2D texture, Vector2 position, int width = 100, int height = 100)
    {
        Texture = texture;
        Position = position;
        Width = width;
        Height = height;
    }

    public virtual void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture, Position, Color.White);
    }

    public virtual void Update() {}
}

public class MainFish : Sprite
{
    public string Direction;


    private Microsoft.Xna.Framework.Content.ContentManager _content;


    public MainFish(Texture2D texture, Vector2 position, string direction, Microsoft.Xna.Framework.Content.ContentManager content) : base(texture,position)
    {
        Direction = direction;
        _content = content;
    }

    public override void Draw(SpriteBatch sb)
    {
        if(Direction == "left")
        {
            Texture = _content.Load<Texture2D>("img/FishLeft");
        }
        else if (Direction == "right")
        {
            Texture = _content.Load<Texture2D>("img/FishRight");
        }
        base.Draw(sb);
    }

    public override void Update()
    {
                // Change position of sprite when you press wasd
        if(Keyboard.GetState().IsKeyDown(Keys.W)) // Up
        {
            Position.Y = Position.Y  - 1;
        }

        if(Keyboard.GetState().IsKeyDown(Keys.S)) // Down
        {
            Position.Y = Position.Y  + 1;
        }
        
        if(Keyboard.GetState().IsKeyDown(Keys.A)) // Left
        {
            Position.X = Position.X  - 1;
            Direction = "left";
        }

        if(Keyboard.GetState().IsKeyDown(Keys.D)) // Right
        {
            Position.X = Position.X  + 1;
            Direction = "right";
        }
    }
}

public class Fish : Sprite
{
    public int ScreenWidth;
    public int ScreenHeight;

    public int moveX;
    public int moveY;

    public Fish(Texture2D texture, Vector2 position, int screenWidth, int screenHeight) : base(texture,position)
    {
        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;
        moveX = moveY = 1;
    }

    public override void Update()
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode()); // get a new seed for a new random number. Dont just rely on CPU clocks!

        if(rand.Next(0,200) == 1)// probability of direction change
        {
            moveX = moveX * -1;
        }

        if(rand.Next(0,200) ==1)// probability of direction change
        {
            moveY = moveY * -1;
        }

        int rightSide = ScreenWidth + 200;
        int bottomSide  = ScreenHeight + 200;

        if(!(Position.X > rightSide || Position.X < 0)) // boundary protection
        {
            Position.X = Position.X  + moveX;
        }

        if(!(Position.Y > bottomSide || Position.Y < 0)) // boundary protection
        {
            Position.Y = Position.Y  + moveY;
        }

    }
}
