namespace Lab2 {
    public class UnitStack {

        private readonly Unit _type;
        private readonly int _count;

        public Unit PType {
            get => _type;
        }

        public int PCount {
            get => _count;
        }

        public UnitStack(Unit type, int cnt) {
            _type = type;
            _count = cnt;
        }

        public UnitStack(UnitStack st) {
            _type = st._type;
            _count = st._count;
        }

        public new string GetType() {
            return _type.GetStringType();
        }

    }
}