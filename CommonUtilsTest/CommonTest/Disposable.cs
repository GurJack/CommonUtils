using System;

namespace CommonUtils
{
    public abstract class Disposable : IDisposable
    {
        private bool _isDisposed = false;//Надо создать и для потомка
        protected virtual void Dispose(bool disposing) // Override
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                // Очищаем ресурсы
            }
            _isDisposed = true;
            //base.Dispose(disposing); // Вызываем дя родителя
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Disposable()
        {
            Dispose(false);
        }
    }
}
