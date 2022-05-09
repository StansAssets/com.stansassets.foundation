namespace StansAssets.Foundation.Async{
    internal struct YieldType<T> {
        public static readonly int Index;
        static YieldType(){
            Index = YieldsCount.value++;
        }
    }
}

