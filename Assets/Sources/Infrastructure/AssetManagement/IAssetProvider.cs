using Sources.Infrastructure.Services;
using UnityEngine;

namespace Sources.Infrastructure.AssetManagement
{
  public interface IAssetProvider:IService
  {
    GameObject Instantiate(string path, Vector3 at);
    GameObject Instantiate(string path);
  }
}