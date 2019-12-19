namespace NSOFunction
{
    public enum WaterCharacter
    {
        /// <summary>
        /// G1 - สถานที่นี้มีผู้อยู่อาศัยติดต่อกันนานเกิน 3 เดือนใช่หรือไม่ 
        /// </summary>
        IsHouseHold = 1,

        /// <summary>
        /// G2 - ผู้ที่อาศัยอยู่ในสถานที่นี้มีพื้นที่ปลกูพืชเลี้ยงสัตว์หรือท้าการเกษตรเองใช่หรือไม่ 
        /// </summary>
        IsAgriculture = 2,

        /// <summary>
        /// G3 - สถานที่นี้ใช้เพื่อทำการผลิตใช่หรือไม่ 
        /// </summary>
        IsFactorial = 3,
        
        /// <summary>
        /// G4 - สถานที่นี้ใช้เพื่อทำการบริการใช่หรือไม่ 
        /// </summary>
        IsCommercial = 4

    }
}