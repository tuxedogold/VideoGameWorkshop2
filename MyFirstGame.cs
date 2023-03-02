using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using System.Collections.Generic;
using System;
using System.Linq;
namespace MyFirstGame;

public class MyFirstGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private MainFish _mainFish;

    private List<Sprite> _otherFish;

    private List<Star> _stars;

    private WinSprite _winSprite;
    private LoseSprite _loseSprite;

    public MyFirstGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.ToggleFullScreen();

        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _otherFish = new List<Sprite>();
        _stars = new List<Star>();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    /*Instantiate your sprites here*/
    protected override void LoadContent()
    {
         Random rand = new Random(Guid.NewGuid().GetHashCode());
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var halfwidth = GraphicsDevice.Viewport.Bounds.Width / 2;
        var halfheight = GraphicsDevice.Viewport.Bounds.Height / 2 ;

        _mainFish = new MainFish(Content.Load<Texture2D>("img/FishLeft"),
                                                        new Vector2(halfwidth,GraphicsDevice.Viewport.Bounds.Height),
                                                        "left",
                                                        Content);
        for(var x = 0; x < 3; x++)
        {
            var fish = new Fish(Content.Load<Texture2D>("img/RedFish"),
                                                            new Vector2(rand.Next(0,GraphicsDevice.Viewport.Bounds.Width-100),rand.Next(0,GraphicsDevice.Viewport.Bounds.Height - 100)), //(X,Y). top left corner X goes left, Y goes down
                                                            GraphicsDevice.Viewport.Bounds.Width,
                                                            GraphicsDevice.Viewport.Bounds.Height);
            while(IsInCollision(_mainFish,fish)) // Don't start the game in a losing position
            {
                fish.Position = new Vector2(rand.Next(0,GraphicsDevice.Viewport.Bounds.Width-100),rand.Next(0,GraphicsDevice.Viewport.Bounds.Height-100));
            }

            _otherFish.Add(fish);
        }

       
        for(var x = 0; x < 5; x++)
        {
            var star = new Star(Content.Load<Texture2D>("img/star"),new Vector2(rand.Next(0,GraphicsDevice.Viewport.Bounds.Width-100),rand.Next(0,GraphicsDevice.Viewport.Bounds.Height-100)));

            while(IsInCollision(_mainFish,star)) // Don't start the game having collected a star already
            {
                star.Position = new Vector2(rand.Next(0,GraphicsDevice.Viewport.Bounds.Width-100),rand.Next(0,GraphicsDevice.Viewport.Bounds.Height-100));
            }
            _stars.Add(star);
        }
        
        _winSprite = new WinSprite(Content.Load<Texture2D>("img/Smile"),
                                                        new Vector2(halfwidth-300,halfheight-230));

        
        _loseSprite = new LoseSprite(Content.Load<Texture2D>("img/FishBones"),
                                                        new Vector2(halfwidth-400,halfheight-350));
    }

    // input related logic
    protected override void Update(GameTime gameTime)
    {
        _mainFish.Update();
        _winSprite.Update();
        _loseSprite.Update();
        foreach(var sprite in _otherFish)
        {
            sprite.Update();
        }
        foreach(var sprite in _stars)
        {
            sprite.Update();
        }

        if(Keyboard.GetState().IsKeyDown(Keys.Escape)) // Escape key, exit game
        {
            System.Environment.Exit(0); // exit code zero indicats there was no error
        }

        base.Update(gameTime);
    }

    // game logic
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _mainFish.Draw(_spriteBatch);
        foreach(var sprite in _otherFish)
        {
            sprite.Draw(_spriteBatch);
            if(IsInCollision(_mainFish,sprite))
            {
                _mainFish.Show = false;
                _loseSprite.Show = true;
            }
        }
        foreach(var sprite in _stars)
        {
            sprite.Draw(_spriteBatch);
            if(IsInCollision(_mainFish,sprite))
            {
                sprite.Show=false;
            }
        }
        if(_stars.Count(x=>x.Show == false) == _stars.Count())
        {
                _mainFish.Show = false;
                _winSprite.Show = true;
        }

        _winSprite.Draw(_spriteBatch);
        _loseSprite.Draw(_spriteBatch);

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
    public bool Show;
    public Sprite(Texture2D texture, Vector2 position, int width = 100, int height = 100)
    {
        Texture = texture;
        Position = position;
        Width = width;
        Height = height;
        Show = true;
    }

    public virtual void Draw(SpriteBatch sb)
    {
        if(Show)
        {
            sb.Draw(Texture, Position, Color.White);
        }
    }

    public virtual void Update() {}
}

public class MainFish : Sprite
{
    public string Direction;

    private Microsoft.Xna.Framework.Content.ContentManager _content;


    public MainFish(Texture2D texture, Vector2 position,  string direction,
    Microsoft.Xna.Framework.Content.ContentManager content) : base(texture,position)
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
public class Star : Sprite
{
    public Star(Texture2D texture, Vector2 position) : base(texture,position) 
    { 
    }
}

public class WinSprite : Sprite
{
    public WinSprite(Texture2D texture, Vector2 position) : base(texture,position) 
    { 
        Show = false;
    }
}

public class LoseSprite : Sprite
{
    public LoseSprite(Texture2D texture, Vector2 position) : base(texture,position) 
    { 
        Show = false;
    }
}
