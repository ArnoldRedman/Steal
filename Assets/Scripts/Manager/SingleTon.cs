public class SingleTon<T> where T : new()  //没有继承 mono  所谓的单例 只有一个实例 我们可以把数据放到单例上
{
    private static T instance;//私有的静态属性 存实例   

    public static T Instance //公开的静态属性  停供给外面访问  暴露了一个new的实例  
    {
        get
        {
            if (instance == null)
                instance = new T();
            return instance;
        }
    }

}
