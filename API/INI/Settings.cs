using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vi.Extensions.String;

namespace Vi
{
    /// <summary>
    /// 
    /// </summary>
    public class Settings: Vi.Types.Profile
    {
        /// <summary>
        /// Main CTor: sets the fileName.
        /// </summary>
        /// <param name="fileName"></param>
        public Settings(string fileName = null) : base(fileName) { }

        ///////////// <summary>
        ///////////// 
        ///////////// </summary>
        ///////////// <param name="section"></param>
        ///////////// <param name="location"></param>
        //////////public void Write(string section, System.Drawing.Point location)
        //////////{
        //////////    this.Write(section, "Location", new int[] { location.X, location.Y });
        //////////}

        ///////////// <summary>
        ///////////// 
        ///////////// </summary>
        ///////////// <param name="section"></param>
        ///////////// <param name="size"></param>
        //////////public void Write(string section, System.Drawing.Size size)
        //////////{
        //////////    this.Write(section, "Size", new int[] { size.Width, size.Height });
        //////////}

        /////////// <summary>
        /////////// 
        /////////// </summary>
        /////////// <param name="section"></param>
        /////////// <param name="default"></param>
        /////////// <returns></returns>
        ////////public System.Drawing.Point ReadLocation(string section, System.Drawing.Point @default)
        ////////{
        ////////    try
        ////////    {
        ////////        var values = this.Read(section, "Location", new int[] { @default.X, @default.Y });
        ////////        return new System.Drawing.Point(values[0], values[1]);
        ////////    }
        ////////    catch (System.Exception se)
        ////////    {
        ////////        return new System.Drawing.Point(0, 0);
        ////////    }
        ////////}

        /////////// <summary>
        /////////// 
        /////////// </summary>
        /////////// <param name="section"></param>
        /////////// <param name="default"></param>
        /////////// <returns></returns>
        ////////public System.Drawing.Size ReadSize(string section, System.Drawing.Size @default)
        ////////{
        ////////    try
        ////////    {
        ////////        var values = this.Read(section, "Size", new int[] { @default.Width, @default.Height });
        ////////        return new System.Drawing.Size(values[0], values[1]);
        ////////    }
        ////////    catch (System.Exception se)
        ////////    {
        ////////        return new System.Drawing.Size(10, 10);
        ////////    }
        ////////}

    }
}
