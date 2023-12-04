﻿using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using EmotesAPI;
using Generics.Dynamics;
using System.Security;
using System.Security.Permissions;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UIElements;
using System.Net.NetworkInformation;
using System.Text;
using GameNetcodeStuff;
using System.IO;
using MonoMod.RuntimeDetour;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
internal static class AnimationReplacements
{
    internal static GameObject g;
    internal static void RunAll()
    {
        ChangeAnims();
        //TODO stuff
        //On.RoR2.UI.HUD.Awake += (orig, self) =>
        //{
        //    orig(self);
        //    g = GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotewheel/emotewheel.prefab"));
        //    foreach (var item in g.GetComponentsInChildren<TextMeshProUGUI>())
        //    {
        //        var money = self.moneyText.targetText;
        //        item.font = money.font;
        //        item.fontMaterial = money.fontMaterial;
        //        item.fontSharedMaterial = money.fontSharedMaterial;
        //    }
        //    g.transform.SetParent(self.mainContainer.transform);
        //    g.transform.localPosition = new Vector3(0, 0, 0);
        //    var s = g.AddComponent<EmoteWheel>();
        //    foreach (var item in g.GetComponentsInChildren<Transform>())
        //    {
        //        if (item.gameObject.name.StartsWith("Emote"))
        //        {
        //            s.gameObjects.Add(item.gameObject);
        //        }
        //        if (item.gameObject.name.StartsWith("MousePos"))
        //        {
        //            s.text = item.gameObject;
        //        }
        //        if (item.gameObject.name == "Center")
        //        {
        //            s.joy = item.gameObject.GetComponent<UnityEngine.UI.Image>();
        //        }
        //        if (item.gameObject.name == "CurrentEmote")
        //        {
        //            EmoteWheel.dontPlayButton = item.gameObject;
        //        }
        //    }


        //    if (CustomEmotesAPI.audioContainers.Count == 0)
        //    {
        //        GameObject audioContainerHolder = new GameObject();
        //        audioContainerHolder.name = "Audio Container Holder";
        //        UnityEngine.Object.DontDestroyOnLoad(audioContainerHolder);
        //        foreach (var item in BoneMapper.startEvents)
        //        {
        //            GameObject aObject = new GameObject();
        //            if (item[0] != "")
        //            {
        //                aObject.name = $"{item[0]}_AudioContainer";
        //            }
        //            var container = aObject.AddComponent<AudioContainer>();
        //            aObject.transform.SetParent(audioContainerHolder.transform);
        //            CustomEmotesAPI.audioContainers.Add(aObject);
        //        }
        //    }
        //};
    }
    internal static bool setup = false;

    public static void DebugBones(string resource, int pos = 0)
    {
        //TODO stuff
        //StringBuilder sb = new StringBuilder();
        //var fab = Addressables.LoadAssetAsync<GameObject>(resource).WaitForCompletion();
        //sb.Append($"{fab.ToString()}\n");
        //var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
        //sb.Append($"rendererererer: {meshes[pos]}\n");
        //sb.Append($"bone count: {meshes[pos].bones.Length}\n");
        //sb.Append($"mesh count: {meshes.Length}\n");
        //sb.Append($"root bone: {meshes[pos].rootBone.name}\n");
        //sb.Append($"{resource}:\n");
        //if (meshes[pos].bones.Length == 0)
        //{
        //    sb.Append("No bones");
        //}
        //else
        //{
        //    sb.Append("[");
        //    foreach (var bone in meshes[pos].bones)
        //    {
        //        sb.Append($"'{bone.name}', ");
        //    }
        //    sb.Remove(sb.Length - 2, 2);
        //    sb.Append("]");
        //}
        //sb.Append("\n\n");
        //DebugClass.Log(sb.ToString());
    }
    internal static void Import(string prefab, string skeleton, bool hidemesh = true)
    {
        Assets.Load<GameObject>(skeleton).GetComponent<Animator>().runtimeAnimatorController = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commando.prefab")).GetComponent<Animator>().runtimeAnimatorController;
        //TODO stuff
        //AnimationReplacements.ApplyAnimationStuff(Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion(), Assets.Load<GameObject>(skeleton), 0, hidemesh, revertBonePositions: true);
    }
    public static void DebugBones(GameObject fab)
    {
        var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
        StringBuilder sb = new StringBuilder();
        sb.Append($"rendererererer: {meshes[0]}\n");
        sb.Append($"bone count: {meshes[0].bones.Length}\n");
        sb.Append($"mesh count: {meshes.Length}\n");
        sb.Append($"root bone: {meshes[0].rootBone.name}\n");
        sb.Append($"{fab.ToString()}:\n");
        if (meshes[0].bones.Length == 0)
        {
            sb.Append("No bones");
        }
        else
        {
            sb.Append("[");
            foreach (var bone in meshes[0].bones)
            {
                sb.Append($"'{bone.name}', ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
        }
        sb.Append("\n\n");
        DebugClass.Log(sb.ToString());
    }
    internal static void ChangeAnims()
    {



        string[] words = new string[] {".." };
        foreach (var word in words)
        {
            foreach (var item in Resources.LoadAll<UnityEngine.Object>(word))
            {
                DebugClass.Log($"{word}--- [{item}]");
            }
        }

        //TODO stuff
        //On.RoR2.SurvivorCatalog.Init += (orig) =>
        //{
        //    orig();
        //    if (!setup)
        //    {
        //        setup = true;

        //        //TODO stuff
        //        //ApplyAnimationStuff(RoR2Content.Survivors.Mage.bodyPrefab, "@CustomEmotesAPI_customemotespackage:assets/animationreplacements/artificer.prefab");
        //        //RoR2Content.Survivors.Mage.bodyPrefab.GetComponentInChildren<BoneMapper>().scale = .9f;


        //        //CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Brother/BrotherBody.prefab").WaitForCompletion(), Assets.Load<GameObject>("@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/brother.prefab"));
        //        //CustomEmotesAPI.ImportArmature(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Brother/BrotherHurtBody.prefab").WaitForCompletion(), Assets.Load<GameObject>("@CustomEmotesAPI_enemyskeletons:assets/myprioritiesarestraightnt/brother.prefab"));

        //    }
        //};
    }
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, string resource, int pos = 0)
    {
        GameObject animcontroller = Assets.Load<GameObject>(resource);
        ApplyAnimationStuff(bodyPrefab, animcontroller, pos);
    }
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, GameObject animcontroller, int pos = 0, bool hidemeshes = true, bool jank = false, bool revertBonePositions = false)
    {
        try
        {
            if (!animcontroller.GetComponentInChildren<Animator>().avatar.isHuman)
            {
                DebugClass.Log($"{animcontroller}'s avatar isn't humanoid, please fix it in unity!");
                return;
            }
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had issue checking if avatar was humanoid: {e}");
            throw;
        }
        try
        {
            if (hidemeshes)
            {
                foreach (var item in animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    item.sharedMesh = null;
                }
                foreach (var item in animcontroller.GetComponentsInChildren<MeshFilter>())
                {
                    item.sharedMesh = null;
                }
            }
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had trouble while hiding meshes: {e}");
            throw;
        }
        //TODO stuff
        //Transform modelTransform;
        //if (bodyPrefab.GetComponent<ModelLocator>())
        //{
        //    modelTransform = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
        //}
        //else
        //{
        //    modelTransform = bodyPrefab.GetComponentInChildren<Animator>().transform;
        //}
        //try
        //{
        //    animcontroller.transform.parent = modelTransform;
        //    animcontroller.transform.localPosition = Vector3.zero;
        //    animcontroller.transform.localEulerAngles = Vector3.zero;
        //    animcontroller.transform.localScale = Vector3.one;
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had trouble setting emote skeletons parent: {e}");
        //    throw;
        //}

        //SkinnedMeshRenderer smr1;
        //SkinnedMeshRenderer smr2;
        //try
        //{
        //    smr1 = animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had trouble setting emote skeletons SkinnedMeshRenderer: {e}");
        //    throw;
        //}
        //try
        //{
        //    smr2 = modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had trouble setting the original skeleton's skinned mesh renderer: {e}");
        //    throw;
        //}
        //try
        //{
        //    int matchingBones = 0;
        //    while (true)
        //    {
        //        foreach (var smr1bone in smr1.bones) //smr is SkinnedMeshRenderer
        //        {
        //            foreach (var smr2bone in smr2.bones)
        //            {
        //                if (smr1bone.name == smr2bone.name)
        //                {
        //                    matchingBones++;
        //                }
        //            }
        //        }
        //        if (matchingBones < 1 && pos + 1 < modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>().Length)
        //        {
        //            pos++;
        //            smr2 = modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
        //            matchingBones = 0;
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had issue while checking matching bones: {e}");
        //    throw;
        //}
        //var test = animcontroller.AddComponent<BoneMapper>();
        //try
        //{
        //    test.jank = jank;
        //    test.smr1 = smr1;
        //    test.smr2 = smr2;
        //    test.bodyPrefab = bodyPrefab;
        //    test.a1 = modelTransform.GetComponentInChildren<Animator>();
        //    test.a2 = animcontroller.GetComponentInChildren<Animator>();
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had issue when setting up BoneMapper settings 1: {e}");
        //    throw;
        //}
        //try
        //{
        //    var nuts = Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/bandit.prefab");
        //    float banditScale = Vector3.Distance(nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
        //    float currScale = Vector3.Distance(animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
        //    test.scale = currScale / banditScale;
        //    test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
        //    test.model = modelTransform.gameObject;
        //}
        //catch (Exception e)
        //{
        //    DebugClass.Log($"Had issue when setting up BoneMapper settings 2: {e}");
        //    throw;
        //}
        //test.revertTransform = revertBonePositions;
    }
}
public struct JoinSpot
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
    public JoinSpot(string _name, Vector3 _position, Vector3 _rotation, Vector3 _scale)
    {
        name = _name;
        position = _position;
        rotation = _rotation;
        scale = _scale;
    }
    public JoinSpot(string _name, Vector3 _position)
    {
        name = _name;
        position = _position;
        rotation = Vector3.zero;
        scale = Vector3.one;
    }
}
public class CustomAnimationClip : MonoBehaviour
{
    public AnimationClip[] clip, secondaryClip; //DONT SUPPORT MULTI CLIP ANIMATIONS TO SYNC     //but why not? how hard could it be, I'm sure I left that note for a reason....  //it was for a reason, but it works now
    public bool looping;
    public string wwiseEvent;
    public bool syncronizeAudio;
    public List<HumanBodyBones> soloIgnoredBones;
    public List<HumanBodyBones> rootIgnoredBones;
    public bool dimAudioWhenClose;
    public bool stopOnAttack;
    public bool stopOnMove;
    public bool visibility;
    public int startPref, joinPref;
    public JoinSpot[] joinSpots;
    public bool useSafePositionReset;
    public string customName;
    public Action<BoneMapper> customPostEventCodeSync;
    public Action<BoneMapper> customPostEventCodeNoSync;


    public bool syncronizeAnimation;
    public int syncPos;
    public static List<float> syncTimer = new List<float>();
    public static List<int> syncPlayerCount = new List<int>();
    public static List<List<bool>> uniqueAnimations = new List<List<bool>>();
    public bool vulnerableEmote = false;

    internal CustomAnimationClip(AnimationClip[] _clip, bool _loop/*, bool _shouldSyncronize = false*/, string[] _wwiseEventName = null, string[] _wwiseStopEvent = null, HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip[] _secondaryClip = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false, int startPreference = -1, int joinPreference = -1, JoinSpot[] _joinSpots = null, bool safePositionReset = false, string customName = "NO_CUSTOM_NAME", Action<BoneMapper> _customPostEventCodeSync = null, Action<BoneMapper> _customPostEventCodeNoSync = null)
    {
        if (rootBonesToIgnore == null)
            rootBonesToIgnore = new HumanBodyBones[0];
        if (soloBonesToIgnore == null)
            soloBonesToIgnore = new HumanBodyBones[0];
        clip = _clip;
        secondaryClip = _secondaryClip;
        looping = _loop;
        //syncronizeAnimation = _shouldSyncronize;
        dimAudioWhenClose = dimWhenClose;
        stopOnAttack = stopWhenAttack;
        stopOnMove = stopWhenMove;
        visibility = visible;
        joinPref = joinPreference;
        startPref = startPreference;
        customPostEventCodeSync = _customPostEventCodeSync;
        customPostEventCodeNoSync = _customPostEventCodeNoSync;
        //int count = 0;
        //float timer = 0;
        //if (_wwiseEventName != "" && _wwiseStopEvent == "")
        //{
        //    //DebugClass.Log($"Error #2: wwiseEventName is declared but wwiseStopEvent isn't skipping sound implementation for [{clip.name}]");
        //}
        //else if (_wwiseEventName == "" && _wwiseStopEvent != "")
        //{
        //    //DebugClass.Log($"Error #3: wwiseStopEvent is declared but wwiseEventName isn't skipping sound implementation for [{clip.name}]");
        //}
        //else if (_wwiseEventName != "")
        //{
        //    //if (!_shouldSyncronize)
        //    //{
        //    BoneMapper.stopEvents.Add(_wwiseStopEvent);
        //    //}
        //    wwiseEvent = _wwiseEventName;
        //}
        string[] wwiseEvents;
        string[] wwiseStopEvents;
        if (_wwiseEventName == null)
        {
            wwiseEvents = new string[] { "" };
            wwiseStopEvents = new string[] { "" };
        }
        else
        {
            wwiseEvents = _wwiseEventName;
            wwiseStopEvents = _wwiseStopEvent;
        }
        BoneMapper.stopEvents.Add(wwiseStopEvents);
        BoneMapper.startEvents.Add(wwiseEvents);
        if (soloBonesToIgnore.Length != 0)
        {
            soloIgnoredBones = new List<HumanBodyBones>(soloBonesToIgnore);
        }
        else
        {
            soloIgnoredBones = new List<HumanBodyBones>();
        }

        if (rootBonesToIgnore.Length != 0)
        {
            rootIgnoredBones = new List<HumanBodyBones>(rootBonesToIgnore);
        }
        else
        {
            rootIgnoredBones = new List<HumanBodyBones>();
        }
        syncronizeAnimation = syncAnim;
        syncronizeAudio = syncAudio;
        syncPos = syncTimer.Count;
        syncTimer.Add(0);
        syncPlayerCount.Add(0);
        List<bool> bools = new List<bool>();
        for (int i = 0; i < _clip.Length; i++)
        {
            bools.Add(false);
        }
        uniqueAnimations.Add(bools);

        if (_joinSpots == null)
            _joinSpots = new JoinSpot[0];
        joinSpots = _joinSpots;
        this.useSafePositionReset = safePositionReset;
        this.customName = customName;
    }
}
public struct WorldProp
{
    internal GameObject prop;
    internal JoinSpot[] joinSpots;
    public WorldProp(GameObject _prop, JoinSpot[] _joinSpots)
    {
        prop = _prop;
        joinSpots = _joinSpots;
    }
}
public class AudioObject : MonoBehaviour
{
    internal int spot;
    internal int playerCount;
    internal GameObject audioObject;
    internal int activeObjectsSpot;
}
public class AudioContainer : MonoBehaviour
{
    internal List<GameObject> playingObjects = new List<GameObject>();
}
public class BoneMapper : MonoBehaviour
{
    public static List<string[]> stopEvents = new List<string[]>();
    public static List<string[]> startEvents = new List<string[]>();
    internal List<GameObject> audioObjects = new List<GameObject>();
    public SkinnedMeshRenderer smr1, smr2;
    public Animator a1, a2;
    //TODO Replace health component with actual health component or something
    //public HealthComponent h;
    public List<BonePair> pairs = new List<BonePair>();
    public float timer = 0;
    public GameObject model;
    List<string> ignore = new List<string>();
    bool twopart = false;
    public static Dictionary<string, CustomAnimationClip> animClips = new Dictionary<string, CustomAnimationClip>();
    public CustomAnimationClip currentClip = null;
    public string currentClipName = "none";
    public string prevClipName = "none";
    public CustomAnimationClip prevClip = null;
    internal static float Current_MSX = 69;
    internal static List<BoneMapper> allMappers = new List<BoneMapper>();
    internal static List<WorldProp> allWorldProps = new List<WorldProp>();
    public bool local = false;
    internal static bool moving = false;
    internal static bool attacking = false;
    public bool jank = false;
    public List<GameObject> props = new List<GameObject>();
    public float scale = 1.0f;
    internal int desiredEvent = 0;
    public int currEvent = 0;
    public float autoWalkSpeed = 0;
    public bool overrideMoveSpeed = false;
    public bool autoWalk = false;
    public GameObject currentEmoteSpot = null;
    public GameObject reservedEmoteSpot = null;
    public bool worldProp = false;
    public bool ragdolling = false;
    public GameObject bodyPrefab;
    public int uniqueSpot = -1;
    public bool preserveProps = false;
    public bool preserveParent = false;
    internal bool useSafePositionReset = false;
    public List<EmoteLocation> emoteLocations = new List<EmoteLocation>();
    List<string> dontAnimateUs = new List<string>();
    public bool enableAnimatorOnDeath = true;
    public bool revertTransform = false;
    public bool oneFrameAnimatorLeeWay = false;
    //TODO Needed this for something
    //public CharacterBody mapperBody;

    public void PlayAnim(string s, int pos, int eventNum)
    {
        desiredEvent = eventNum;
        PlayAnim(s, pos);
    }
    public void PlayAnim(string s, int pos)
    {
        prevClipName = currentClipName;
        if (s != "none")
        {
            if (!animClips.ContainsKey(s))
            {
                DebugClass.Log($"No emote bound to the name [{s}]");
                return;
            }
            try
            {
                animClips[s].ToString();
            }
            catch (Exception)
            {
                CustomEmotesAPI.Changed(s, this);
                return;
            }
        }
        a2.enabled = true;
        if (a2.transform.parent.GetComponent<InverseKinematics>() && a2.name != "loader")
        {
            a2.transform.parent.GetComponent<InverseKinematics>().enabled = false;
        }
        dontAnimateUs.Clear();
        try
        {
            currentClip.clip[0].ToString();
            try
            {
                if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
                {
                    CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                    RemoveAudioObject();
                }
                if (stopEvents[currentClip.syncPos][currEvent] != "")
                {
                    audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                    if (!currentClip.syncronizeAudio)
                    {
                        //TODO stuff
                        //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], this.gameObject);
                    }


                    if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                    {
                        foreach (var item in allMappers)
                        {
                            if (!item.audioObjects[currentClip.syncPos])
                            {
                                DebugClass.Log($"The audioObject was null????????");
                            }
                            item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                        }
                        //TODO stuff
                        //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], CustomEmotesAPI.audioContainers[currentClip.syncPos]);
                    }
                }
                if (uniqueSpot != -1 && CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot])
                {
                    CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot] = false;
                    uniqueSpot = -1;
                }
            }
            catch (Exception e)
            {
                DebugClass.Log($"had issue turning off audio before new audio played: {e}\n Notable items for debugging: [currentClip: {currentClip}] [currentClip.syncPos: {currentClip.syncPos}] [currEvent: {currEvent}] [stopEvents[currentClip.syncPos]: {stopEvents[currentClip.syncPos]}] [CustomEmotesAPI.audioContainers[currentClip.syncPos]: {CustomEmotesAPI.audioContainers[currentClip.syncPos]}] [uniqueSpot: {uniqueSpot}] [CustomAnimationClip.uniqueAnimations[currentClip.syncPos]: {CustomAnimationClip.uniqueAnimations[currentClip.syncPos]}]");
            }
        }
        catch (Exception)
        {

        }
        currEvent = 0;
        currentClipName = s;
        if (s != "none")
        {
            prevClip = currentClip;
            currentClip = animClips[s];
            try
            {
                currentClip.clip[0].ToString();
            }
            catch (Exception)
            {
                return;
            }
            if (pos == -2)
            {
                if (CustomAnimationClip.syncPlayerCount[animClips[s].syncPos] == 0)
                {
                    pos = animClips[s].startPref;
                }
                else
                {
                    pos = animClips[s].joinPref;
                }
            }
            if (pos == -2)
            {
                for (int i = 0; i < CustomAnimationClip.uniqueAnimations[currentClip.syncPos].Count; i++)
                {
                    if (!CustomAnimationClip.uniqueAnimations[currentClip.syncPos][i])
                    {
                        pos = i;
                        uniqueSpot = pos;
                        CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot] = true;
                        break;
                    }
                }
                if (uniqueSpot == -1)
                {
                    pos = -1;
                }
            }
            if (pos == -1)
            {
                pos = UnityEngine.Random.Range(0, currentClip.clip.Length);
            }
            LockBones();
        }
        if (s == "none")
        {
            a2.Play("none", -1, 0f);
            twopart = false;
            prevClip = currentClip;
            currentClip = null;
            NewAnimation(null);
            CustomEmotesAPI.Changed(s, this);

            return;
        }
        AnimatorOverrideController animController = new AnimatorOverrideController(a2.runtimeAnimatorController);
        if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
        {
            CustomAnimationClip.syncPlayerCount[currentClip.syncPos]++;
            //DebugClass.Log($"--------------  adding audio object {currentClip.syncPos}");
            AddAudioObject();
        }
        if (currentClip.syncronizeAnimation && CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1)
        {
            CustomAnimationClip.syncTimer[currentClip.syncPos] = 0;
        }
        if (startEvents[currentClip.syncPos][currEvent] != "")
        {
            if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1 && currentClip.syncronizeAudio)
            {
                if (desiredEvent != -1)
                    currEvent = desiredEvent;
                else
                    currEvent = UnityEngine.Random.Range(0, startEvents[currentClip.syncPos].Length);
                foreach (var item in allMappers)
                {
                    item.currEvent = currEvent;
                }
                if (currentClip.customPostEventCodeSync != null)
                {
                    currentClip.customPostEventCodeSync.Invoke(this);
                }
                else
                {
                    //TODO stuff
                    //AkSoundEngine.PostEvent(startEvents[currentClip.syncPos][currEvent], CustomEmotesAPI.audioContainers[currentClip.syncPos]);
                }
            }
            else if (!currentClip.syncronizeAudio)
            {
                currEvent = UnityEngine.Random.Range(0, startEvents[currentClip.syncPos].Length);
                if (currentClip.customPostEventCodeNoSync != null)
                {
                    currentClip.customPostEventCodeNoSync.Invoke(this);
                }
                else
                {
                    //TODO stuff
                    //AkSoundEngine.PostEvent(startEvents[currentClip.syncPos][currEvent], this.gameObject);
                }
            }
            audioObjects[currentClip.syncPos].transform.localPosition = Vector3.zero;
        }
        SetAnimationSpeed(1);
        if (currentClip.secondaryClip != null && currentClip.secondaryClip.Length != 0)
        {
            if (true)
            {
                if (CustomAnimationClip.syncTimer[currentClip.syncPos] > currentClip.clip[pos].length)
                {
                    animController["Floss"] = currentClip.secondaryClip[pos];
                    a2.runtimeAnimatorController = animController;
                    a2.Play("Loop", -1, ((CustomAnimationClip.syncTimer[currentClip.syncPos] - currentClip.clip[pos].length) % currentClip.secondaryClip[pos].length) / currentClip.secondaryClip[pos].length);
                }
                else
                {
                    animController["Dab"] = currentClip.clip[pos];
                    animController["nobones"] = currentClip.secondaryClip[pos];
                    a2.runtimeAnimatorController = animController;
                    a2.Play("PoopToLoop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
                }
            }
        }
        else if (currentClip.looping)
        {
            animController["Floss"] = currentClip.clip[pos];
            a2.runtimeAnimatorController = animController;
            if (currentClip.clip[pos].length != 0)
            {
                a2.Play("Loop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
            }
            else
            {
                a2.Play("Loop", -1, 0);
            }
        }
        else
        {
            animController["Default Dance"] = currentClip.clip[pos];
            a2.runtimeAnimatorController = animController;
            a2.Play("Poop", -1, (CustomAnimationClip.syncTimer[currentClip.syncPos] % currentClip.clip[pos].length) / currentClip.clip[pos].length);
        }
        twopart = false;
        NewAnimation(currentClip.joinSpots);
        CustomEmotesAPI.Changed(s, this);
    }
    public void SetAnimationSpeed(float speed)
    {
        a2.speed = speed;
    }
    internal void NewAnimation(JoinSpot[] locations)
    {
        try
        {
            try
            {
                //TODO stuff
                //emoteLocations.Clear();
                //autoWalkSpeed = 0;
                //autoWalk = false;
                //overrideMoveSpeed = false;
                //if (parentGameObject && !preserveParent)
                //{
                //    Vector3 OHYEAHHHHHH = mapperBody.gameObject.transform.position;
                //    OHYEAHHHHHH = (TeleportHelper.FindSafeTeleportDestination(mapperBody.gameObject.transform.position, mapperBody, RoR2Application.rng)) ?? OHYEAHHHHHH;
                //    Vector3 result = OHYEAHHHHHH;
                //    RaycastHit raycastHit = default(RaycastHit);
                //    Ray ray = new Ray(OHYEAHHHHHH + Vector3.up * 2f, Vector3.down);
                //    float maxDistance = 4f;
                //    if (Physics.SphereCast(ray, mapperBody.radius, out raycastHit, maxDistance, LayerIndex.world.mask))
                //    {
                //        result.y = ray.origin.y - raycastHit.distance;
                //    }
                //    float bodyPrefabFootOffset = Util.GetBodyPrefabFootOffset(bodyPrefab);
                //    result.y += bodyPrefabFootOffset;
                //    if (!useSafePositionReset)
                //    {
                //        Rigidbody rigidbody = mapperBody.gameObject.GetComponent<Rigidbody>();
                //        rigidbody.AddForce(new Vector3(0, 1, 0), ForceMode.VelocityChange);
                //        mapperBody.gameObject.transform.position += new Vector3(.1f, 2, .1f);
                //        mapperBody.GetComponent<ModelLocator>().modelBaseTransform.position += new Vector3(0, 10, 0);
                //    }
                //    else
                //    {
                //        mapperBody.gameObject.transform.position = result;
                //        mapperBody.GetComponent<ModelLocator>().modelBaseTransform.position = result;
                //    }
                //    parentGameObject = null;
                //}
            }
            catch (Exception)
            {
            }
            try
            {
                useSafePositionReset = currentClip.useSafePositionReset;
            }
            catch (Exception)
            {
                useSafePositionReset = true;
            }
            try
            {
                //TODO stuff
                //if (preserveParent)
                //{
                //    preserveParent = false;
                //}
                //else
                //{
                //    mapperBody.GetComponent<ModelLocator>().modelBaseTransform.localEulerAngles = Vector3.zero;
                //    if (mapperBody.GetComponent<CharacterDirection>())
                //    {
                //        mapperBody.GetComponent<CharacterDirection>().enabled = true;
                //    }
                //    if (ogScale != new Vector3(-69, -69, -69))
                //    {
                //        transform.parent.localScale = ogScale;
                //        ogScale = new Vector3(-69, -69, -69);
                //    }
                //    if (ogLocation != new Vector3(-69, -69, -69))
                //    {
                //        mapperBody.GetComponent<ModelLocator>().modelBaseTransform.localPosition = ogLocation;
                //        ogLocation = new Vector3(-69, -69, -69);
                //    }
                //    if (mapperBody.GetComponent<Collider>())
                //    {
                //        mapperBody.GetComponent<Collider>().enabled = true;
                //    }
                //    if (mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>() && !mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>().enabled)
                //    {
                //        Vector3 desired = mapperBody.gameObject.transform.position;
                //        mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>().enabled = true;
                //        mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>().SetPosition(desired);
                //    }
                //}
            }
            catch (Exception)
            {
            }
            if (preserveProps)
            {
                preserveProps = false;
            }
            else
            {
                foreach (var item in props)
                {
                    if (item)
                        GameObject.Destroy(item);
                }
                props.Clear();
            }
            if (locations != null)
            {
                for (int i = 0; i < locations.Length; i++)
                {
                    SpawnJoinSpot(locations[i]);
                }
            }
        }
        catch (Exception e)
        {
            DebugClass.Log($"error during new animation: {e}");
        }
    }
    public void ScaleProps()
    {
        foreach (var item in props)
        {
            if (item)
            {
                Transform t = item.transform.parent;
                item.transform.SetParent(null);
                item.transform.localScale = new Vector3(scale * 1.15f, scale * 1.15f, scale * 1.15f);
                item.transform.SetParent(t);
            }
        }
    }
    //TODO stuff
    //void AddIgnore(DynamicBone dynbone, Transform t)
    //{
    //    for (int i = 0; i < t.childCount; i++)
    //    {
    //        if (!dynbone.m_Exclusions.Contains(t.GetChild(i)))
    //        {
    //            ignore.Add(t.GetChild(i).name);
    //            AddIgnore(dynbone, t.GetChild(i));
    //        }
    //    }
    //}
    void Start()
    {
        if (worldProp)
        {
            return;
        }
        //TODO stuff
        //mapperBody = GetComponentInParent<CharacterModel>().body;
        allMappers.Add(this);
        foreach (var item in startEvents)
        {
            GameObject obj = new GameObject();
            if (item[0] != "")
            {
                obj.name = $"{item[0]}_AudioObject";
            }
            obj.transform.SetParent(transform);
            obj.transform.localPosition = new Vector3(0, -10000, 0);
            audioObjects.Add(obj);
        }
        //TODO stuff
        //foreach (var item in model.GetComponents<DynamicBone>())
        //{
        //    try
        //    {
        //        if (!item.m_Exclusions.Contains(item.m_Root))
        //        {
        //            ignore.Add(item.m_Root.name);
        //        }
        //        AddIgnore(item, item.m_Root);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        if (model.name.StartsWith("mdlLoader"))
        {
            Transform LClav = model.transform, RClav = model.transform;
            foreach (var item in model.GetComponentsInChildren<Transform>())
            {
                if (item.name == "clavicle.l")
                {
                    LClav = item;
                    ignore.Add(LClav.name);
                }
                if (item.name == "clavicle.r")
                {
                    RClav = item;
                    ignore.Add(RClav.name);
                }
            }
            foreach (var item in LClav.GetComponentsInChildren<Transform>())
            {
                ignore.Add(item.name);
            }
            foreach (var item in RClav.GetComponentsInChildren<Transform>())
            {
                ignore.Add(item.name);
            }
        }
        int offset = 0;
        bool nuclear = true;
        if (nuclear)
        {
            int startingXPoint = 0;
            for (int i = 0; i < smr1.bones.Length; i++)
            {
                for (int x = startingXPoint; x < smr2.bones.Length; x++)
                {
                    //DebugClass.Log($"comparing:    {smr1.bones[i].name}     {smr2.bones[x].name}");
                    //DebugClass.Log($"--------------  {smr2bone.gameObject.name}   {smr1bone.gameObject.name}      {smr2bone.GetComponent<ParentConstraint>()}");
                    if (smr1.bones[i].name == smr2.bones[x].name/* + "_CustomEmotesAPIBone"*/ /*smr2.bones[x].name != "head_jnt" && smr2.bones[x].name != "head_jnt_end" && smr2.bones[x].name != "arm_left_jnt" && smr2.bones[x].name != "clavicle_right_jnt" && smr2.bones[x].name != "spine_04_jnt"*/)
                    {
                        startingXPoint = x;
                        //DebugClass.Log($"they are equal!");
                        var s = new ConstraintSource();
                        s.sourceTransform = smr1.bones[i];
                        s.weight = 1;
                        //DebugClass.Log($"{smr2.name}--- i is equal to {x}  ------ {smr2.bones[x].name}");
                        EmoteConstraint e = smr2.bones[x].gameObject.AddComponent<EmoteConstraint>();
                        e.AddSource(ref smr2.bones[x], ref smr1.bones[i]);
                        e.revertTransform = revertTransform;
                        //smr1bone.name = smr1bone.name.Remove(smr1bone.name.Length - "_CustomEmotesAPIBone".Length);
                        break;
                    }
                    if (x == startingXPoint - 1)
                    {
                        break;
                    }
                    if (startingXPoint > 0 && x == smr2.bones.Length - 1)
                    {
                        x = -1;
                    }
                }
            }
        }
        if (jank)
        {
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (smr2.bones[i].gameObject.GetComponent<EmoteConstraint>())
                    {
                        //DebugClass.Log($"-{i}---------{smr2.bones[i].gameObject}");
                        smr2.bones[i].gameObject.GetComponent<EmoteConstraint>().ActivateConstraints();
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"{e}");
                }
            }
            //a1.enabled = false;
        }
        CustomEmotesAPI.MapperCreated(this);
        DebugClass.Log(a2.transform.parent.name);
        if (a2.transform.parent.name == "mdlRocket") //put more jank here if ever needed
        {
            DebugClass.Log(a2.transform.parent.name);
            StartCoroutine(FixRootOnSurvivors());
        }
    }
    public GameObject parentGameObject;
    bool positionLock, rotationLock, scaleLock;
    IEnumerator FixRootOnSurvivors()
    {
        yield return 0;
        PlayAnim("lmao", 0);
        yield return 0;
        PlayAnim("none", 0);
        smr2.transform.parent.gameObject.SetActive(false);
        smr2.transform.parent.gameObject.SetActive(true);
    }
    public void AssignParentGameObject(GameObject youAreTheFather, bool lockPosition, bool lockRotation, bool lockScale, bool scaleAsBandit = true, bool disableCollider = true)
    {
        if (parentGameObject)
        {
            NewAnimation(null);
        }
        //TODO stuff
        //if (!transform.GetComponentInParent<CharacterModel>())
        //{
        //    return;
        //}
        //TODO stuff
        //ogLocation = mapperBody.GetComponent<ModelLocator>().modelBaseTransform.localPosition;
        ogScale = transform.parent.localScale;
        if (scaleAsBandit)
            scaleDiff = ogScale / scale;
        else
            scaleDiff = ogScale;

        //RoR2.Navigation.NodeGraph nodeGraph = SceneInfo.instance.GetNodeGraph(RoR2.Navigation.MapNodeGroup.GraphType.Ground);
        //List<RoR2.Navigation.NodeGraph.Node> nodes = new List<RoR2.Navigation.NodeGraph.Node>(nodeGraph.nodes);
        //nodes.Add(new RoR2.Navigation.NodeGraph.Node
        //{
        //    position = youAreTheFather.transform.position,
        //    forbiddenHulls = HullMask.None,
        //    flags = RoR2.Navigation.NodeFlags.None,
        //});
        //nodeGraph.nodes = nodes.ToArray();

        parentGameObject = youAreTheFather;
        //parentGameObject.transform.position += new Vector3(0, 0.060072f, 0);
        //startingPos = transform.position/* + new Vector3(0, 0.060072f, 0)*/;
        positionLock = lockPosition;
        rotationLock = lockRotation;
        scaleLock = lockScale;
        //TODO stuff
        //if (mapperBody.GetComponent<Collider>())
        //{
        //    mapperBody.GetComponent<Collider>().enabled = !disableCollider;
        //}
        //if (mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>())
        //{
        //    mapperBody.GetComponent<KinematicCharacterController.KinematicCharacterMotor>().enabled = !lockPosition;
        //}
        if (disableCollider && currentEmoteSpot)
        {
            if (currentEmoteSpot.GetComponent<EmoteLocation>().validPlayers != 0)
            {
                currentEmoteSpot.GetComponent<EmoteLocation>().validPlayers--;
            }
            currentEmoteSpot.GetComponent<EmoteLocation>().SetColor();
            currentEmoteSpot = null;
        }
    }
    float interval = 0;
    Vector3 ogScale = new Vector3(-69, -69, -69);
    Vector3 ogLocation = new Vector3(-69, -69, -69);
    Vector3 scaleDiff = Vector3.one;
    float rtpcUpdateTimer = 0;
    void DimmingChecks()
    {
        float closestDimmingSource = 20f;
        foreach (var item in allMappers)// With the 20 Larva moon2 test https://cdn.discordapp.com/attachments/483371638340059156/1100077546256740412/Risk_of_Rain_2_7BN1yOoLBo.jpg
        {                               // this section consumes about 7 of the 60 fps that I lose when they play an emote. Not great, but not terrible either, I will look elsewhere for optimization
            try
            {
                if (item)
                {
                    if (item.a2.enabled && item.currentClip.dimAudioWhenClose)
                    {
                        if (Vector3.Distance(item.transform.parent.position, transform.position) < closestDimmingSource)
                        {
                            closestDimmingSource = Vector3.Distance(item.transform.position, transform.position);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        SetRTPCInDimming(closestDimmingSource);
        //////////////////////////////////////////////////////////////////////////// end of section
    }
    void SetRTPCInDimming(float closestDimmingSource)
    {
        if (closestDimmingSource < 20f && Settings.DimmingSpheres.Value && Settings.EmotesVolume.Value > 0)
        {
            Current_MSX = Mathf.Lerp(Current_MSX, (closestDimmingSource / 20f) * CustomEmotesAPI.Actual_MSX, Time.deltaTime * 3);
            //TODO stuff
            //AkSoundEngine.SetRTPCValue("Volume_MSX", Current_MSX);
        }
        else if (Current_MSX != CustomEmotesAPI.Actual_MSX)
        {
            Current_MSX = Mathf.Lerp(Current_MSX, CustomEmotesAPI.Actual_MSX, Time.deltaTime * 3);
            if (Current_MSX + .01f > CustomEmotesAPI.Actual_MSX)
            {
                Current_MSX = CustomEmotesAPI.Actual_MSX;
            }
            //TODO stuff
            //AkSoundEngine.SetRTPCValue("Volume_MSX", Current_MSX);
        }
    }
    void LocalFunctions()
    {
        //AudioFunctions();
        DimmingChecks();
        try
        {
            if ((attacking && currentClip.stopOnAttack) || (moving && currentClip.stopOnMove))
            {

                CustomEmotesAPI.PlayAnimation("none");
            }
        }
        catch (Exception)
        {
        }
    }
    void GetLocal()
    {
        try
        {
            if (!CustomEmotesAPI.localMapper)
            {
                //TODO stuff
                //var body = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                //TODO stuff
                //if (body.gameObject.GetComponent<ModelLocator>().modelTransform == transform.parent)
                //{
                //    CustomEmotesAPI.localMapper = this;

                //    local = true;


                //    //GameObject g = new GameObject();
                //    //g.name = "AudioContainer";

                //    //if (CustomEmotesAPI.audioContainers.Count == 0)
                //    //{
                //    //    GameObject audioContainerHolder = new GameObject();
                //    //    audioContainerHolder.name = "Audio Container Holder";
                //    //    UnityEngine.Object.DontDestroyOnLoad(audioContainerHolder);
                //    //    foreach (var item in BoneMapper.startEvents)
                //    //    {
                //    //        GameObject aObject = new GameObject();
                //    //        if (item[0] != "")
                //    //        {
                //    //            aObject.name = $"{item[0]}_AudioContainer";
                //    //        }
                //    //        var container = aObject.AddComponent<AudioContainer>();
                //    //        aObject.transform.SetParent(audioContainerHolder.transform);
                //    //        CustomEmotesAPI.audioContainers.Add(aObject);
                //    //    }
                //    //}
                //}
            }
        }
        catch (Exception)
        {
        }
    }
    void TwoPartThing()
    {
        if (a2.GetCurrentAnimatorStateInfo(0).IsName("none"))
        {
            if (!twopart)
            {
                twopart = true;
            }
            else
            {
                if (a2.enabled)
                {
                    if (smr2.transform.parent.gameObject.name == "mdlVoidSurvivor" || smr2.transform.parent.gameObject.name == "mdlMage" || smr2.transform.parent.gameObject.name == "mdlJinx" || smr2.transform.parent.gameObject.name.StartsWith("mdlHouse") || smr2.transform.parent.gameObject.name.StartsWith("mdlLemurian"))
                    {
                        smr2.transform.parent.gameObject.SetActive(false);
                        smr2.transform.parent.gameObject.SetActive(true);
                    }
                    if (!jank)
                    {
                        UnlockBones();
                    }
                }
                //DebugClass.Log($"----------{a1}");
                if (!ragdolling)
                {
                    a1.enabled = true;
                    oneFrameAnimatorLeeWay = true;
                }
                a2.enabled = false;
                if (a2.transform.parent.GetComponent<InverseKinematics>() && a2.name != "loader")
                {
                    a2.transform.parent.GetComponent<InverseKinematics>().enabled = true;
                }
                try
                {
                    currentClip.clip.ToString();
                    CustomEmotesAPI.Changed("none", this);
                    NewAnimation(null);
                    if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
                    {
                        CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                        RemoveAudioObject();
                    }
                    if (stopEvents[currentClip.syncPos][currEvent] != "")
                    {
                        if (!currentClip.syncronizeAudio)
                        {
                            //TODO stuff
                            //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], this.gameObject);
                        }
                        audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);

                        if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                        {
                            foreach (var item in allMappers)
                            {
                                item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                            }
                            //TODO stuff
                            //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], CustomEmotesAPI.audioContainers[currentClip.syncPos]);
                        }
                    }
                    prevClip = currentClip;
                    currentClip = null;
                }
                catch (Exception)
                {
                }
            }
        }
        else
        {
            //a1.enabled = false;
            twopart = false;
        }
    }
    void HealthAndAutoWalk()
    {
        //TODO stuff
        //if (autoWalkSpeed != 0)
        //{
        //    CharacterModel model = transform.GetComponentInParent<CharacterModel>();
        //    if (model)
        //    {
        //        if (overrideMoveSpeed)
        //        {
        //            model.body.moveSpeed = autoWalkSpeed;
        //        }
        //        CharacterMotor motor = model.body.GetComponent<CharacterMotor>();
        //        CharacterDirection direction = model.body.GetComponent<CharacterDirection>();
        //        if (autoWalk && motor && direction)
        //        {
        //            motor.moveDirection = direction.forward * autoWalkSpeed;
        //        }
        //    }
        //}
        //if (h)
        //{
        //    if (h.health <= 0)
        //    {
        //        UnlockBones(enableAnimatorOnDeath);
        //        GameObject.Destroy(gameObject);
        //    }
        //}
    }
    void WorldPropAndParent()
    {
        if (parentGameObject)
        {
            if (positionLock)
            {
                //TODO stuff
                //mapperBody.gameObject.transform.position = parentGameObject.transform.position + new Vector3(0, 1, 0);
                //mapperBody.GetComponent<ModelLocator>().modelBaseTransform.position = parentGameObject.transform.position;
                //CharacterMotor motor = mapperBody.GetComponent<CharacterMotor>();
                //Rigidbody rigidbody = mapperBody.GetComponent<Rigidbody>();
                //if (motor)
                //{
                //    motor.velocity = Vector3.zero;
                //}
                //else if (rigidbody)
                //{
                //    rigidbody.velocity = Vector3.zero;
                //}
            }
            if (rotationLock)
            {
                //TODO stuff
                //CharacterDirection direction = mapperBody.GetComponent<CharacterDirection>();
                //if (direction)
                //{
                //    mapperBody.GetComponent<CharacterDirection>().enabled = false;
                //}
                //mapperBody.GetComponent<ModelLocator>().modelBaseTransform.eulerAngles = parentGameObject.transform.eulerAngles;
            }
            if (scaleLock)
            {
                transform.parent.localScale = new Vector3(parentGameObject.transform.localScale.x * scaleDiff.x, parentGameObject.transform.localScale.y * scaleDiff.y, parentGameObject.transform.localScale.z * scaleDiff.z);
            }
        }
    }
    void Update()
    {
        if (worldProp)
        {
            return;
        }
        WorldPropAndParent();
        if (local)
        {
            LocalFunctions();
        }
        else
        {
            GetLocal();
        }
        TwoPartThing();
        HealthAndAutoWalk();
    }
    public int SpawnJoinSpot(JoinSpot joinSpot)
    {
        props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/emotejoiner/JoinVisual.prefab")));
        props[props.Count - 1].transform.SetParent(transform);
        //Vector3 scal = transform.lossyScale;
        //props[props.Count - 1].transform.localPosition = new Vector3(joinSpot.position.x / scal.x, joinSpot.position.y / scal.y, joinSpot.position.z / scal.z);
        //props[props.Count - 1].transform.localEulerAngles = joinSpot.rotation;
        //props[props.Count - 1].transform.localScale = new Vector3(joinSpot.scale.x / scal.x, joinSpot.scale.y / scal.y, joinSpot.scale.z / scal.z);
        props[props.Count - 1].name = joinSpot.name;
        //foreach (var rend in props[props.Count - 1].GetComponentsInChildren<SkinnedMeshRenderer>())
        //{
        //    rend.material.shader = CustomEmotesAPI.standardShader;
        //}
        EmoteLocation location = props[props.Count - 1].AddComponent<EmoteLocation>();
        location.joinSpot = joinSpot;
        location.owner = this;
        emoteLocations.Add(location);
        return props.Count - 1;
    }
    public void JoinEmoteSpot()
    {
        if (reservedEmoteSpot)
        {
            if (currentEmoteSpot)
            {
                if (currentEmoteSpot.GetComponent<EmoteLocation>().validPlayers != 0)
                {
                    currentEmoteSpot.GetComponent<EmoteLocation>().validPlayers--;
                }
                currentEmoteSpot.GetComponent<EmoteLocation>().SetColor();

            }
            currentEmoteSpot = reservedEmoteSpot;
            reservedEmoteSpot = null;
        }
        int spot = 0;
        for (; spot < currentEmoteSpot.transform.parent.GetComponentsInChildren<EmoteLocation>().Length; spot++)
        {
            if (currentEmoteSpot.transform.parent.GetComponentsInChildren<EmoteLocation>()[spot] == currentEmoteSpot.GetComponent<EmoteLocation>())
            {
                break;
            }
        }

        if (currentEmoteSpot.GetComponent<EmoteLocation>().owner.worldProp)
        {
            //TODO stuff
            //new SyncSpotJoinedToHost(mapperBody.GetComponent<NetworkIdentity>().netId, currentEmoteSpot.transform.GetComponentInParent<NetworkIdentity>().netId, true, spot).Send(R2API.Networking.NetworkDestination.Server);
        }
        else
        {
            //TODO stuff
            //new SyncSpotJoinedToHost(mapperBody.GetComponent<NetworkIdentity>().netId, currentEmoteSpot.transform.parent.GetComponentInParent<CharacterModel>().body.GetComponent<NetworkIdentity>().netId, false, spot).Send(R2API.Networking.NetworkDestination.Server);
        }
    }
    public void RemoveProp(int propPos)
    {
        GameObject.Destroy(props[propPos]);
    }
    public void SetAutoWalk(float speed, bool overrideBaseMovement, bool autoWalk)
    {
        autoWalkSpeed = speed;
        overrideMoveSpeed = overrideBaseMovement;
        this.autoWalk = autoWalk;
    }
    public void SetAutoWalk(float speed, bool overrideBaseMovement)
    {
        autoWalkSpeed = speed;
        overrideMoveSpeed = overrideBaseMovement;
        autoWalk = true;
    }
    void FixedUpdate()
    {
        //if (autoWalkSpeed != 0)
        //{
        //    if (overrideMoveSpeed)
        //    {
        //        transform.GetComponentInParent<CharacterModel>().body.GetComponent<InputBankTest>().moveVector = transform.GetComponentInParent<CharacterModel>().body.GetComponent<CharacterDirection>().forward * autoWalkSpeed;
        //    }
        //    else
        //    {
        //        transform.GetComponentInParent<CharacterModel>().body.GetComponent<CharacterMotor>().moveDirection = transform.GetComponentInParent<CharacterModel>().body.GetComponent<CharacterDirection>().forward * autoWalkSpeed;
        //    }
        //}
    }
    internal IEnumerator waitForTwoFramesThenDisableA1()
    {
        yield return new WaitForEndOfFrame(); //haha we only wait for one frame lmao
        a1.enabled = false;
    }
    void AddAudioObject()
    {
        CustomEmotesAPI.audioContainers[currentClip.syncPos].GetComponent<AudioContainer>().playingObjects.Add(this.gameObject);
    }
    void RemoveAudioObject()
    {
        try
        {
            CustomEmotesAPI.audioContainers[currentClip.syncPos].GetComponent<AudioContainer>().playingObjects.Remove(this.gameObject);
        }
        catch (Exception e)
        {
            DebugClass.Log($"failed to remove object {this.gameObject} from playingObjects: {e}");
        }
    }
    void OnDestroy()
    {
        try
        {
            currentClip.clip[0].ToString();
            NewAnimation(null);
            if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
            {
                if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] > 0)
                {
                    CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                    RemoveAudioObject();
                }
            }
            if (stopEvents[currentClip.syncPos][currEvent] != "")
            {
                if (!currentClip.syncronizeAudio)
                {
                    //TODO stuff
                    //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], this.gameObject);
                }
                audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);

                if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 0 && currentClip.syncronizeAudio)
                {
                    foreach (var item in allMappers)
                    {
                        item.audioObjects[currentClip.syncPos].transform.localPosition = new Vector3(0, -10000, 0);
                    }
                    //TODO stuff
                    //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], CustomEmotesAPI.audioContainers[currentClip.syncPos]);
                }
                //TODO stuff
                //AkSoundEngine.PostEvent(stopEvents[currentClip.syncPos][currEvent], audioObjects[currentClip.syncPos]);
            }
            if (uniqueSpot != -1 && CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot])
            {
                CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot] = false;
                uniqueSpot = -1;
            }
            BoneMapper.allMappers.Remove(this);
            prevClip = currentClip;
            currentClip = null;
        }
        catch (Exception e)
        {
            //DebugClass.Log($"Had issues when destroying bonemapper: {e}");
            BoneMapper.allMappers.Remove(this);
        }
    }
    public void UnlockBones(bool animatorEnabled = true)
    {
        //CustomEmotesAPI.instance.wackActive(this);
        for (int i = 0; i < smr2.bones.Length; i++)
        {
            try
            {
                if (smr2.bones[i].gameObject.GetComponent<EmoteConstraint>())
                {
                    smr2.bones[i].gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                }
            }
            catch (Exception)
            {
                break;
            }
        }
        a1.enabled = animatorEnabled;
    }
    public void LockBones()
    {
        bool footL = false;
        bool footR = false;
        bool upperLegR = false;
        bool upperLegL = false;
        bool lowerLegR = false;
        bool lowerLegL = false;
        foreach (var item in currentClip.soloIgnoredBones)
        {
            if (item == HumanBodyBones.LeftFoot)
            {
                footL = true;
            }
            if (item == HumanBodyBones.RightFoot)
            {
                footR = true;
            }
            if (item == HumanBodyBones.LeftLowerLeg)
            {
                lowerLegL = true;
            }
            if (item == HumanBodyBones.LeftUpperLeg)
            {
                upperLegL = true;
            }
            if (item == HumanBodyBones.RightLowerLeg)
            {
                lowerLegR = true;
            }
            if (item == HumanBodyBones.RightUpperLeg)
            {
                upperLegR = true;
            }
            if (a2.GetBoneTransform(item))
                dontAnimateUs.Add(a2.GetBoneTransform(item).name);
        }
        foreach (var item in currentClip.rootIgnoredBones)
        {

            if (item == HumanBodyBones.LeftUpperLeg || item == HumanBodyBones.Hips)
            {
                upperLegL = true;
                lowerLegL = true;
                footL = true;
            }
            if (item == HumanBodyBones.RightUpperLeg || item == HumanBodyBones.Hips)
            {
                upperLegR = true;
                lowerLegR = true;
                footR = true;
            }
            if (a2.GetBoneTransform(item))
                dontAnimateUs.Add(a2.GetBoneTransform(item).name);
            foreach (var bone in a2.GetBoneTransform(item).GetComponentsInChildren<Transform>())
            {

                dontAnimateUs.Add(bone.name);
            }
        }
        bool left = upperLegL && lowerLegL && footL;
        bool right = upperLegR && lowerLegR && footR;
        Transform LeftLegIK = null;
        Transform RightLegIK = null;
        if (!jank)
        {
            StartCoroutine(waitForTwoFramesThenDisableA1());
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (right && (smr2.bones[i].gameObject.ToString() == "IKLegTarget.r (UnityEngine.GameObject)" || smr2.bones[i].gameObject.ToString() == "FootControl.r (UnityEngine.GameObject)"))
                    {
                        RightLegIK = smr2.bones[i];
                    }
                    else if (left && (smr2.bones[i].gameObject.ToString() == "IKLegTarget.l (UnityEngine.GameObject)" || smr2.bones[i].gameObject.ToString() == "FootControl.l (UnityEngine.GameObject)"))
                    {
                        LeftLegIK = smr2.bones[i];
                    }
                    if (smr2.bones[i].gameObject.GetComponent<EmoteConstraint>() && !dontAnimateUs.Contains(smr2.bones[i].name))
                    {
                        //DebugClass.Log($"-{i}---------{smr2.bones[i].gameObject}");
                        smr2.bones[i].gameObject.GetComponent<EmoteConstraint>().ActivateConstraints(); //this is like, 99% of fps loss right here. Unfortunate
                    }
                    else if (dontAnimateUs.Contains(smr2.bones[i].name))
                    {
                        //DebugClass.Log($"dontanimateme-{i}---------{smr2.bones[i].gameObject}");
                        smr2.bones[i].gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"{e}");
                }
            }
            if (left && LeftLegIK)//we can leave ik for the legs
            {
                if (LeftLegIK.gameObject.GetComponent<EmoteConstraint>())
                    LeftLegIK.gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                foreach (var item in LeftLegIK.gameObject.GetComponentsInChildren<Transform>())
                {
                    if (item.gameObject.GetComponent<EmoteConstraint>())
                        item.gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                }
            }
            if (right && RightLegIK)
            {
                if (RightLegIK.gameObject.GetComponent<EmoteConstraint>())
                    RightLegIK.gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                foreach (var item in RightLegIK.gameObject.GetComponentsInChildren<Transform>())
                {
                    if (item.gameObject.GetComponent<EmoteConstraint>())
                        item.gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                }
            }
        }
        else
        {
            StartCoroutine(waitForTwoFramesThenDisableA1());
        }
    }
}
public class BonePair
{
    public Transform original, newiginal;
    public BonePair(Transform n, Transform o)
    {
        newiginal = n;
        original = o;
    }

    public void test()
    {

    }
}

internal static class Pain
{
    internal static Transform FindBone(SkinnedMeshRenderer mr, string name)
    {
        foreach (var item in mr.bones)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        DebugClass.Log($"couldnt find bone [{name}]");
        return mr.bones[0];
    }

    internal static Transform FindBone(List<Transform> bones, string name)
    {
        foreach (var item in bones)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        DebugClass.Log($"couldnt find bone [{name}]");
        return bones[0];
    }
}
