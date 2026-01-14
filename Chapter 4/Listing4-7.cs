struct ProjectedInt 

{ 

    int projectedValue; //if RealValue is 31, this is 33 

    public int RealValue 

    { 

        get => projectedValue + 2; 

        set => projectedValue = value - 2; 

    } 

} 

//and then we use ‘ProjectedInt ammo;’ instead of ‘int ammo;’ 