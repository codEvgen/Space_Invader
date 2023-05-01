using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Space_Invaders;

public class Player
{
    public bool IsPlayerDead { get; private set; }
    private readonly Sprite _sprite;

    //private Sprite _sprite2;
    private Vector2f _startPositionSpriteFirst;

    private readonly ShootingManager _shootingManager;
    private readonly Keyboard.Key _shootingButton;
    private readonly PlayerMovement _playerMovement;

    // private Vector2f _startPositionSpriteSecond;

    public Player(ShootingManager shootingManager, Keyboard.Key shootingButton, Texture texture,
        Vector2f playerSpawnPosition, PlayerMovement playerMovement)
    {
        _sprite = new Sprite(texture);
        _sprite.Position = playerSpawnPosition;
        _shootingManager = shootingManager;
        _shootingButton = shootingButton;
        _playerMovement = playerMovement;
    }

    public void Update()
    {
        Move();
        if (Keyboard.IsKeyPressed(_shootingButton))
        {
            var bulletSpawnPosition = GetBulletSpawnPosition();
            _shootingManager.TryShoot(bulletSpawnPosition);
        }

        _shootingManager.Update();
    }

    public void Draw(RenderWindow window)
    {
        window.Draw(_sprite);
        _shootingManager.Draw(window);
        //window.Draw(_sprite2);
    }
    public List<Bullet> GetBullets()
    {
        return _shootingManager.Bullets;
    }
    public void DestroyBullet(Bullet bullet)
    {
        _shootingManager.Bullets.Remove(bullet);
    }
    public FloatRect GetGlobalBounds()
    {
        return _sprite.GetGlobalBounds();
    }
    public void Destroy()
    {
        IsPlayerDead = true;
    }

    private void Move()
    {
        var newPosition = _playerMovement.GetNewPosition(_sprite.Position);
        _sprite.Position = newPosition;
    }

    

    private Vector2f GetBulletSpawnPosition()
    {
        var halfSpriteSizeX = new Vector2f(_sprite.TextureRect.Width / 2f, 0f);
        var bulletSpawnPosition = _sprite.Position + halfSpriteSizeX;
        return bulletSpawnPosition;
    }

    // private void MoveSecondPlayer()
    // {
    //     bool shouldMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.Left);
    //     bool shouldMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.Right);
    //     bool shouldMoveUp = Keyboard.IsKeyPressed(Keyboard.Key.Up);
    //     bool shouldMoveDown = Keyboard.IsKeyPressed(Keyboard.Key.Down);
    //     bool shouldMoveCenter=Keyboard.IsKeyPressed(Keyboard.Key.Enter);
    //     bool shouldMove = shouldMoveLeft || shouldMoveRight || shouldMoveUp || shouldMoveDown;
    //     if (shouldMoveCenter)
    //     {
    //         var positionCenter = _sprite2.Position;
    //         positionCenter = _startPositionSpriteSecond;
    //         _sprite2.Position = positionCenter;
    //     }
    //     
    //     if (!shouldMove)
    //     {
    //         return;
    //     }
    //     var position = _sprite2.Position;
    //     if (shouldMoveLeft)
    //     {
    //         position.X -= PLAYER_SPEED;
    //     }

    //     if (shouldMoveRight)
    //     {
    //         position.X += PLAYER_SPEED;
    //     }

    //     if (shouldMoveUp)
    //     {
    //         position.Y -= PLAYER_SPEED;
    //     }

    //     if (shouldMoveDown)
    //     {
    //         position.Y += PLAYER_SPEED;
    //     }
    //     _sprite2.Position = position;
    // }
}