namespace Framework.Extension
{
    public class ExtensionHelper : SingletonDestroyable<ExtensionHelper>
    {
		void OnDestroy()
		{
			StopAllCoroutines();
		}
	}
}