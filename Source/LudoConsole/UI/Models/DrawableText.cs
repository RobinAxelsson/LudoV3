namespace LudoConsole.UI.Models
{
    public class DrawableText : DrawableBase
    {
        public DrawableText(int coordX, int coordY, char chr)
        {
            CoordinateX = coordX;
            CoordinateY = coordY;
            Chars = chr.ToString();
        }
    }
}