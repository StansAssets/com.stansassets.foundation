internal struct YieldType<T> {
    public static readonly int Index;
    static YieldType(){
        Index = YieldsCount.value++;
    }
}
