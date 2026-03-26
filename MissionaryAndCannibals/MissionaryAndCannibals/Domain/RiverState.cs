namespace MissionaryAndCannibals.Domain;

public class RiverState
{
    public int MissionariesLeft { get; }
    public int CannibalsLeft { get; }
    public bool BoatIsLeft { get; }

    public int MissionariesRight => 3 - MissionariesLeft;
    public int CannibalsRight => 3 - CannibalsLeft;

    public RiverState(int mLeft, int cLeft, bool boatLeft)
    {
        MissionariesLeft = mLeft;
        CannibalsLeft = cLeft;
        BoatIsLeft = boatLeft;
    }

    public bool IsValid()
    {
        // Must be non-negative and not exceeding 3
        if (MissionariesLeft < 0 || CannibalsLeft < 0 || MissionariesRight < 0 || CannibalsRight < 0)
            return false;
            
        if (MissionariesLeft > 3 || CannibalsLeft > 3)
            return false;

        // On left bank: Missionaries must not be outnumbered by cannibals
        if (MissionariesLeft > 0 && CannibalsLeft > MissionariesLeft)
            return false;

        // On right bank: Missionaries must not be outnumbered by cannibals
        if (MissionariesRight > 0 && CannibalsRight > MissionariesRight)
            return false;

        return true;
    }

    public bool IsGoal() => MissionariesLeft == 0 && CannibalsLeft == 0 && !BoatIsLeft;

    public override bool Equals(object? obj)
    {
        if (obj is not RiverState other) return false;
        return MissionariesLeft == other.MissionariesLeft &&
               CannibalsLeft == other.CannibalsLeft &&
               BoatIsLeft == other.BoatIsLeft;
    }

    public override int GetHashCode() => HashCode.Combine(MissionariesLeft, CannibalsLeft, BoatIsLeft);
    
    public override string ToString() => $"L:({MissionariesLeft}M, {CannibalsLeft}C), Boat:{(BoatIsLeft ? "Left" : "Right")}, R:({MissionariesRight}M, {CannibalsRight}C)";
}
