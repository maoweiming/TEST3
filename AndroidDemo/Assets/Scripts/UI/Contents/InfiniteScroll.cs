using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InfiniteScroll : MonoBehaviour, IDropHandler
{


    //方向  顶端
    public enum Direction
    {
        Top,
        Button
    }
    //委托机制  fillItem 添满格子   pull 拉
    public event Action<int, GameObject> FillItem = delegate { };
    public event Action<Direction> PullLoad = delegate { };

    //http://www.360doc.com/content/15/1205/14/25502502_518083734.shtml 
    //项目设置
    [Header("Item settings")]
    public GameObject prefab;
    public int height = 110;

    //填充
    [Header("Padding")]
    public int top = 10;
    public int botton = 10;
    //间距
    public int spacing = 2;

    //标签
    [Header("Lables")]
    public string topPullLabel = "上拉刷新";
    public string topReleaseLabel = "释放加载";
    public string bottonPullLabel = "上拉刷新";
    public string bottonReleaseLabel = "释放加载";

    //方向
    [Header("Directions")]
    public bool isPullTop = true;
    public bool isPullBotton = true;

    //间距范围
    [Header("Pull coefficient")]
    [Range(0.01f, 0.1f)]
    public float pullValue = 0.05f;

    //表示将原本显示在面板上的序列化值隐藏起来。
    [HideInInspector]
    public Text topLabel;
    [HideInInspector]
    public Text bottonLabel;


    private ScrollRect _scroll;
    private RectTransform _content;
    private RectTransform[] _rects;
    private GameObject[] _views;
    private bool _isCanLoadUp;
    private bool _isCanLoadDown;
    private int _previousPosition;
    private int _count;


    void Awake()
    {
        _scroll = GetComponent<ScrollRect>();
        _scroll.onValueChanged.AddListener(OnScrollChange);
        _content = _scroll.viewport.transform.GetChild(0).GetComponent<RectTransform>();
        CreateView();
        CreateLabel();
    }


    // Update is called once per frame
    void Update()
    {
        if (_count == 0)
            return;


        float _topPosition = _content.anchoredPosition.y - spacing;
        if (_topPosition <= 0f && _rects[0].anchoredPosition.y < -top - 10f)
        {
            //初始化数据
            InitData(_count);
            return;
        }

        if (_topPosition < 0f)
        {
            return;
        }

        int postion = Mathf.FloorToInt(_topPosition / (height + spacing));
        if (_previousPosition == postion)
        {
            return;
        }


        /////出错啊 啊 
        if (postion > _previousPosition)
        {
            if (postion - _previousPosition > 1)
            
                postion = _previousPosition + 1;

                int newposition = postion % _views.Length;
                newposition--;

            /////////////这一定要分开 不然不会循环/////////////////////
                if (newposition < 0)
                {
                    newposition = _views.Length - 1;

                }


                    int index = postion + _views.Length - 1;

                    if (index < _count)
                    {
                        Vector2 pos = _rects[newposition].anchoredPosition;
                        pos.y = -(top + index * spacing+ index * height);
                        _rects[newposition].anchoredPosition = pos;
                        FillItem(index, _views[newposition]);
                    }
                
            
        }
        else
        {
            if (_previousPosition - postion > 1)
            {
                postion = _previousPosition - 1;
            }

                int newIndex = postion % _views.Length;
                Vector2 pos = _rects[newIndex].anchoredPosition;
                pos.y = -(top + postion * spacing + postion * height);
                _rects[newIndex].anchoredPosition = pos;
                FillItem(postion, _views[newIndex]);

           
        }

        _previousPosition = postion;

    }




    /// <summary>
    /// 滚动的变化  加载的上拉刷新  释放加载
    /// </summary>
    /// <param name="vector"></param>
    void OnScrollChange(Vector2 vector)
    {
        //系数
        float coef = _count / _views.Length;
        Debug.Log(coef+"系数");
        float y = 0f;
        _isCanLoadUp = false;
        _isCanLoadDown = false;


        if (vector.y > 1f)
        {
            y = (vector.y - 1f) * coef;
            Debug.Log(vector.y);
        }
        else if (vector.y < 0f)
        {
            y = vector.y * coef;
        }
       

        if (y > pullValue && isPullTop)
        {
            topLabel.gameObject.SetActive(true);
            Debug.Log(topLabel.name);
            //显示标签 上拉刷新
            topLabel.text = topPullLabel;
            if (y > pullValue * 2)
            {
                //释放加载
                topLabel.text = topReleaseLabel;
                _isCanLoadUp = true;
            }
        }
        else
        {
            topLabel.gameObject.SetActive(false);
        }



        if (y < -pullValue && isPullBotton)
        {
            bottonLabel.gameObject.SetActive(true);
            bottonLabel.text = bottonPullLabel;
            if(y<-pullValue*2)
            {
                bottonLabel.text = bottonReleaseLabel;
                _isCanLoadDown = true;
            }
        }
        else
        {
            bottonLabel.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 接口的实现
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if (_isCanLoadUp)
            PullLoad(Direction.Top);
        else if (_isCanLoadDown)
            PullLoad(Direction.Button);


        _isCanLoadUp = false;
        _isCanLoadDown = false;
    }
   
    /// <summary>
    /// 创建视图 生产预设体
    /// </summary>
    void CreateView()
    {
        GameObject clone;
        RectTransform rect;

        int fillCount = Mathf.RoundToInt((float)Screen.currentResolution.height / height) + 2;
        _views = new GameObject[fillCount];
        Debug.Log("生成预设体，设置父物体");


        for (int i = 0; i < fillCount; i++)
        {
            //生产预设体   设置父物体
            clone = (GameObject)Instantiate(prefab, Vector3.zero, Quaternion.identity);
            clone.transform.SetParent(_content);
            clone.transform.localScale = Vector3.one;
            clone.transform.localPosition = Vector3.zero;
            clone.name = clone.name.Replace("(Clone)", "");
         
            //获取位置
            rect = clone.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 1f);
            rect.anchorMin = new Vector2(0f, 1f);
            rect.anchorMax = new Vector2(1f, 1f);
            rect.offsetMax = new Vector2(0f, 0f);
            //height
            rect.offsetMin = new Vector2(0f, -height);
            _views[i] = clone;
        }
        _rects = new RectTransform[_views.Length];
        Debug.Log(_rects);
        for (int i=0;i<_views.Length;i++)
        {
            _rects[i] = _views[i].gameObject.GetComponent<RectTransform>();
        }
    }

    /// <summary>
    /// 创建标签  不显示Text 上拉刷新  释放加载
    /// </summary>
    void CreateLabel()
    {
        GameObject topText = new GameObject("TopLabel");
        topText.transform.SetParent(_scroll.viewport.transform);
        topLabel = topText.AddComponent<Text>();
        topLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        topLabel.fontSize = 24;
        topLabel.transform.localScale = Vector3.one;
        topLabel.alignment = TextAnchor.MiddleCenter;
        topLabel.text = topPullLabel;


        RectTransform rect = topLabel.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, 1f);
        rect.anchorMin = new Vector2(0f, 1f);
        rect.anchorMax = new Vector2(1f, 1f);
        rect.offsetMax = new Vector2(0f, 0f);
        rect.offsetMin = new Vector2(0f, -55f);
        rect.anchoredPosition3D = Vector3.zero;

        topText.SetActive(false);

        GameObject bottomText = new GameObject("BottomLabel");
        bottomText.transform.SetParent(_scroll.viewport.transform);
        bottonLabel= bottomText.AddComponent<Text>();
        bottonLabel.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        bottonLabel.fontSize = 24;
        bottonLabel.transform.localScale = Vector3.one;
        bottonLabel.alignment = TextAnchor.MiddleCenter;
        bottonLabel.text = bottonPullLabel;
        bottonLabel.transform.position=Vector3.zero;
        rect = bottonLabel.GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, 0f);
        rect.anchorMin = new Vector2(0f, 0f);
        rect.anchorMax = new Vector2(1f, 0f);
        rect.offsetMax = new Vector2(0f, 55f);
        rect.offsetMin = new Vector2(0f, 0f);
        rect.anchoredPosition3D = Vector3.zero;
        bottomText.SetActive(false);	
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    /// <param name="count"></param>
  public void InitData(int count)
    {
        _previousPosition = 0;
        _count = count;
        float h = height * count * 1f + top + botton + (count == 0 ? 0 : ((count - 1) * spacing));

        _content.sizeDelta = new Vector2(_content.sizeDelta.x, h);

        Vector2 pos = _content.anchoredPosition;
        pos.y = 0f;

        _content.anchoredPosition = pos;
        int y = top;
        bool showed = false;

        Debug.Log("生成预设体===实体==="+_views.Length);
        for (int i = 0; i < _views.Length;i++)
        {

            showed = i < count;
            _views[i].gameObject.SetActive(showed);
            pos = _rects[i].anchoredPosition;
            pos.y = -y;
            pos.x = 0f;
            _rects[i].anchoredPosition = pos;
            y += spacing + height;
            if (i + 1 > _count)
            {
                continue;             
            }
            //显示字体的代码
            FillItem(i, _views[i]);
          

        }                                 
    }


    public void ApplyDataTo(int count, int newCount, Direction direction)
    {
        _count = count;
        float newHeight = height * count * 1f + top + botton+ (count == 0 ? 0 : ((count - 1) * spacing));
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, newHeight);
        Vector2 pos = _content.anchoredPosition;
        if (direction == Direction.Top)
        {
            pos.y = (height + spacing) * newCount;
            _previousPosition = newCount;
        }
        else
            pos.y = newHeight - (height * spacing) * newCount - (float)Screen.currentResolution.height;
        _content.anchoredPosition = pos;
        float _topPosition = _content.anchoredPosition.y - spacing;
        int index = Mathf.FloorToInt(_topPosition / (height + spacing));
        int all = top + index * spacing + index * height;
        for (int i = 0; i < _views.Length; i++)
        {
            int newIndex = index % _views.Length;
            FillItem(index, _views[newIndex]);
            pos = _rects[newIndex].anchoredPosition;
            pos.y = -all;
            _rects[newIndex].anchoredPosition = pos;
            all += spacing + height;
            index++;
            if (index == _count)
                break;
        }
    }
}
