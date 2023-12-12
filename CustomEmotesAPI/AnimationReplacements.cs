﻿using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Animations;
using EmotesAPI;
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
using UnityEngine.SceneManagement;
using Unity.Netcode;
using LethalEmotesAPI;
using UnityEngine.Audio;

[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
internal static class AnimationReplacements
{
    internal static GameObject g;
    internal static void RunAll()
    {
        ChangeAnims();
        //TODO hud awake, adding the emote wheel
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
    internal static void Import(GameObject prefab, string skeleton, int[] pos, bool hidemesh = true)
    {
        Assets.Load<GameObject>(skeleton).GetComponent<Animator>().runtimeAnimatorController = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@CustomEmotesAPI_customemotespackage:assets/animationreplacements/commando.prefab")).GetComponent<Animator>().runtimeAnimatorController;
        AnimationReplacements.ApplyAnimationStuff(prefab, GameObject.Instantiate(Assets.Load<GameObject>(skeleton)), pos, hidemesh, revertBonePositions: true);
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

    }
    internal static void ApplyAnimationStuff(GameObject bodyPrefab, string resource, int[] pos)
    {
        GameObject animcontroller = Assets.Load<GameObject>(resource);
        ApplyAnimationStuff(bodyPrefab, animcontroller, pos);
    }

    internal static void ApplyAnimationStuff(GameObject bodyPrefab, GameObject animcontroller, int[] pos, bool hidemeshes = true, bool jank = false, bool revertBonePositions = false)
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

        Transform modelTransform;
        modelTransform = bodyPrefab.GetComponentInChildren<Animator>().transform;
        try
        {
            animcontroller.transform.parent = modelTransform;
            animcontroller.transform.localPosition = Vector3.zero;
            animcontroller.transform.localEulerAngles = new Vector3(90, 0, 0);
            animcontroller.transform.localScale = Vector3.one;
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had trouble setting emote skeletons parent: {e}");
            throw;
        }

        SkinnedMeshRenderer smr1;
        SkinnedMeshRenderer[] smr2 = new SkinnedMeshRenderer[pos.Length];
        try
        {
            smr1 = animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>()[0];
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had trouble setting emote skeletons SkinnedMeshRenderer: {e}");
            throw;
        }
        try
        {
            for (int i = 0; i < pos.Length; i++)
            {
                smr2[i] = bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>()[pos[i]];
            }
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had trouble setting the original skeleton's skinned mesh renderer: {e}");
            throw;
        }


        //since this game is jank and has A UNIQUE SKINNEDMESHRENDERER FOR EACH LOD, I am just going to enforce proper SMR labeling. This probably won't be that big of a deal since I imagine the need for people setting up their own emote skeletons will be FAR less than ROR2
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
        //        if (matchingBones < 1 && pos + 1 < bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>().Length)
        //        {
        //            pos++;
        //            smr2 = bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
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

        var test = animcontroller.AddComponent<BoneMapper>();
        try
        {
            test.jank = jank;
            test.smr1 = smr1;
            test.smr2 = smr2;
            test.bodyPrefab = bodyPrefab;
            test.a1 = modelTransform.GetComponentInChildren<Animator>();
            test.a2 = animcontroller.GetComponentInChildren<Animator>();
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had issue when setting up BoneMapper settings 1: {e}");
            throw;
        }
        try
        {
            var nuts = Assets.Load<GameObject>("assets/customstuff/scavEmoteSkeleton.prefab");
            float banditScale = Vector3.Distance(nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, nuts.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
            float currScale = Vector3.Distance(animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head).position, animcontroller.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).position);
            test.scale = currScale / banditScale;
            test.h = bodyPrefab.GetComponentInChildren<PlayerControllerB>().health;
            test.model = modelTransform.gameObject;
        }
        catch (Exception e)
        {
            DebugClass.Log($"Had issue when setting up BoneMapper settings 2: {e}");
            throw;
        }
        test.revertTransform = revertBonePositions;

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
    public bool lockCameraByDefault = false;

    internal CustomAnimationClip(AnimationClip[] _clip, bool _loop, AudioClip[] primaryAudioClips = null, AudioClip[] secondaryAudioClips = null, HumanBodyBones[] rootBonesToIgnore = null, HumanBodyBones[] soloBonesToIgnore = null, AnimationClip[] _secondaryClip = null, bool dimWhenClose = false, bool stopWhenMove = false, bool stopWhenAttack = false, bool visible = true, bool syncAnim = false, bool syncAudio = false, int startPreference = -1, int joinPreference = -1, JoinSpot[] _joinSpots = null, bool safePositionReset = false, string customName = "NO_CUSTOM_NAME", Action<BoneMapper> _customPostEventCodeSync = null, Action<BoneMapper> _customPostEventCodeNoSync = null, bool lockCameraByDefault = false)
    {
        if (rootBonesToIgnore == null)
            rootBonesToIgnore = new HumanBodyBones[0];
        if (soloBonesToIgnore == null)
            soloBonesToIgnore = new HumanBodyBones[0];
        clip = _clip;
        secondaryClip = _secondaryClip;
        looping = _loop;
        dimAudioWhenClose = dimWhenClose;
        stopOnAttack = stopWhenAttack;
        stopOnMove = stopWhenMove;
        visibility = visible;
        joinPref = joinPreference;
        startPref = startPreference;
        customPostEventCodeSync = _customPostEventCodeSync;
        customPostEventCodeNoSync = _customPostEventCodeNoSync;
        if (primaryAudioClips == null)
        {
            BoneMapper.primaryAudioClips.Add(new AudioClip[] { null });
        }
        else
        {
            BoneMapper.primaryAudioClips.Add(primaryAudioClips);
        }
        if (secondaryAudioClips == null)
        {
            BoneMapper.secondaryAudioClips.Add(new AudioClip[] { null });
        }
        else
        {
            BoneMapper.secondaryAudioClips.Add(secondaryAudioClips);
        }
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
        if (customName != "NO_CUSTOM_NAME")
        {
            BoneMapper.customNamePairs.Add(customName, _clip[0].name);
        }
        BoneMapper.listOfCurrentEmoteAudio.Add(new List<AudioSource>());

        if (!audioLoader)
        {
            DebugClass.Log($"preloading audio, this can take up to 30 seconds unfortunately but prevents microstutters when actually playing.");
            audioLoader = GameObject.Instantiate(Assets.Load<GameObject>("assets/source1.prefab"));
        }
        audioLoader.name = "sugma balls";
        AudioSource a = audioLoader.GetComponent<AudioSource>();
        if (primaryAudioClips != null)
        {
            foreach (var item in primaryAudioClips)
            {
                if (item != null)
                {
                    a.PlayOneShot(item);
                    a.Stop();
                }
            }
        }
        if (secondaryAudioClips != null)
        {
            foreach (var item in secondaryAudioClips)
            {
                if (item != null)
                {
                    a.PlayOneShot(item);
                    a.Stop();
                }
            }
        }

        this.lockCameraByDefault = lockCameraByDefault;
    }
    private static GameObject audioLoader;
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
    public static List<AudioClip[]> primaryAudioClips = new List<AudioClip[]>();
    public static List<AudioClip[]> secondaryAudioClips = new List<AudioClip[]>();
    public GameObject audioObject;
    public SkinnedMeshRenderer smr1;
    public SkinnedMeshRenderer[] smr2;
    public Animator a1, a2;
    public int h;
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
    public PlayerControllerB mapperBody;
    public static bool firstMapperSpawn = true;
    public static List<List<AudioSource>> listOfCurrentEmoteAudio = new List<List<AudioSource>>();
    public EmoteConstraint cameraConstraint;
    public static Dictionary<string, string> customNamePairs = new Dictionary<string, string>();


    public void PlayAnim(string s, int pos, int eventNum)
    {
        desiredEvent = eventNum;
        if (customNamePairs.ContainsKey(s))
        {
            s = customNamePairs[s];
        }
        PlayAnim(s, pos);
    }
    public void PlayAnim(string s, int pos)
    {
        if (customNamePairs.ContainsKey(s))
        {
            s = customNamePairs[s];
        }
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

        dontAnimateUs.Clear();
        try
        {
            currentClip.clip[0].ToString();
            try
            {
                if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
                {
                    CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                }
                audioObject.GetComponent<AudioManager>().Stop();
                if (primaryAudioClips[currentClip.syncPos][currEvent] != null && currentClip.syncronizeAudio)
                {
                    listOfCurrentEmoteAudio[currentClip.syncPos].Remove(audioObject.GetComponent<AudioSource>());
                }
                if (uniqueSpot != -1 && CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot])
                {
                    CustomAnimationClip.uniqueAnimations[currentClip.syncPos][uniqueSpot] = false;
                    uniqueSpot = -1;
                }
            }
            catch (Exception e)
            {
                DebugClass.Log($"had issue turning off audio before new audio played: {e}\n Notable items for debugging: [currentClip: {currentClip}] [currentClip.syncPos: {currentClip.syncPos}] [currEvent: {currEvent}] [uniqueSpot: {uniqueSpot}] [CustomAnimationClip.uniqueAnimations[currentClip.syncPos]: {CustomAnimationClip.uniqueAnimations[currentClip.syncPos]}]");
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
        }
        if (currentClip.syncronizeAnimation && CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1)
        {
            CustomAnimationClip.syncTimer[currentClip.syncPos] = 0;
        }
        if (primaryAudioClips[currentClip.syncPos][currEvent] != null)
        {
            if (CustomAnimationClip.syncPlayerCount[currentClip.syncPos] == 1 && currentClip.syncronizeAudio)
            {
                if (desiredEvent != -1)
                    currEvent = desiredEvent;
                else
                    currEvent = UnityEngine.Random.Range(0, primaryAudioClips[currentClip.syncPos].Length);
                foreach (var item in allMappers)
                {
                    item.currEvent = currEvent;
                }
                if (currentClip.customPostEventCodeSync != null)
                {
                    currentClip.customPostEventCodeSync.Invoke(this);
                }
                //audioObject.GetComponent<AudioManager>().Play(currentClip.syncPos, currEvent, currentClip.looping);
            }
            else if (!currentClip.syncronizeAudio)
            {
                currEvent = UnityEngine.Random.Range(0, primaryAudioClips[currentClip.syncPos].Length);
                if (currentClip.customPostEventCodeNoSync != null)
                {
                    currentClip.customPostEventCodeNoSync.Invoke(this);
                }
                //audioObject.GetComponent<AudioManager>().Play(currentClip.syncPos, currEvent, currentClip.looping);
            }
            audioObject.GetComponent<AudioManager>().Play(currentClip.syncPos, currEvent, currentClip.looping, currentClip.syncronizeAudio);
            if (currentClip.syncronizeAudio && primaryAudioClips[currentClip.syncPos][currEvent] != null)
            {
                listOfCurrentEmoteAudio[currentClip.syncPos].Add(audioObject.GetComponent<AudioSource>());
            }
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
                emoteLocations.Clear();
                autoWalkSpeed = 0;
                autoWalk = false;
                overrideMoveSpeed = false;
                if (parentGameObject && !preserveParent)
                {
                    parentGameObject = null;
                }
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
                if (preserveParent)
                {
                    preserveParent = false;
                }
                else
                {
                    mapperBody.gameObject.transform.localEulerAngles = new Vector3(0, mapperBody.gameObject.transform.localEulerAngles.y, 0);
                    if (ogScale != new Vector3(-69, -69, -69))
                    {
                        mapperBody.transform.localScale = ogScale;
                        ogScale = new Vector3(-69, -69, -69);
                    }
                    foreach (var item in mapperBody.GetComponentsInChildren<Collider>())
                    {
                        item.enabled = true;
                    }
                    if (mapperBody.GetComponent<CharacterController>())
                    {
                        mapperBody.GetComponent<CharacterController>().enabled = true;
                    }
                }
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
    void Start()
    {

        if (worldProp)
        {
            return;
        }
        mapperBody = transform.parent.parent.parent.GetComponent<PlayerControllerB>();
        allMappers.Add(this);

        GameObject obj = GameObject.Instantiate(Assets.Load<GameObject>("assets/source1.prefab"));
        obj.name = $"{name}_AudioObject";
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var source = obj.GetComponent<AudioSource>();
        obj.AddComponent<AudioManager>().Setup(source, this);
        source.playOnAwake = false;
        source.volume = Settings.EmotesVolume.Value / 100f;
        audioObject = obj;

        int offset = 0;
        bool nuclear = true;
        if (nuclear)
        {
            foreach (var smr in smr2)
            {
                int startingXPoint = 0;
                for (int i = 0; i < smr1.bones.Length; i++)
                {
                    for (int x = startingXPoint; x < smr.bones.Length; x++)
                    {
                        //DebugClass.Log($"comparing:    {smr1.bones[i].name}     {smr.bones[x].name}");
                        //DebugClass.Log($"--------------  {smrbone.gameObject.name}   {smr1bone.gameObject.name}      {smrbone.GetComponent<ParentConstraint>()}");
                        if (smr1.bones[i].name == smr.bones[x].name && !smr.bones[x].gameObject.GetComponent<EmoteConstraint>())
                        {
                            startingXPoint = x;
                            //DebugClass.Log($"they are equal!");
                            var s = new ConstraintSource();
                            s.sourceTransform = smr1.bones[i];
                            s.weight = 1;
                            //DebugClass.Log($"{smr.name}--- i is equal to {x}  ------ {smr.bones[x].name}");
                            EmoteConstraint e = smr.bones[x].gameObject.AddComponent<EmoteConstraint>();
                            e.AddSource(ref smr.bones[x], ref smr1.bones[i]);
                            e.revertTransform = revertTransform;
                            break;
                        }
                        if (x == startingXPoint - 1)
                        {
                            break;
                        }
                        if (startingXPoint > 0 && x == smr.bones.Length - 1)
                        {
                            x = -1;
                        }
                    }
                }
            }
        }
        Camera c = mapperBody.GetComponentInChildren<Camera>();
        if (c)
        {
            cameraConstraint = c.transform.parent.gameObject.AddComponent<EmoteConstraint>();
            cameraConstraint.AddSource(c.transform.parent, this.GetComponentInChildren<Animator>().GetBoneTransform(HumanBodyBones.Head));
            cameraConstraint.revertTransform = revertTransform;
        }
        if (jank)
        {
            foreach (var smr in smr2)
            {
                for (int i = 0; i < smr.bones.Length; i++)
                {
                    try
                    {
                        if (smr.bones[i].gameObject.GetComponent<EmoteConstraint>())
                        {
                            //DebugClass.Log($"-{i}---------{smr2.bones[i].gameObject}");
                            smr.bones[i].gameObject.GetComponent<EmoteConstraint>().ActivateConstraints();
                        }
                    }
                    catch (Exception e)
                    {
                        DebugClass.Log($"{e}");
                    }
                }
            }
            //a1.enabled = false;
        }

        CustomEmotesAPI.MapperCreated(this);

    }
    public GameObject parentGameObject;
    bool positionLock, rotationLock, scaleLock;
    public void AssignParentGameObject(GameObject youAreTheFather, bool lockPosition, bool lockRotation, bool lockScale, bool scaleAsBandit = true, bool disableCollider = true)
    {
        if (parentGameObject)
        {
            NewAnimation(null);
        }
        ogScale = mapperBody.transform.localScale;
        if (scaleAsBandit)
            scaleDiff = ogScale / scale;
        else
            scaleDiff = ogScale;

        parentGameObject = youAreTheFather;
        positionLock = lockPosition;
        rotationLock = lockRotation;
        scaleLock = lockScale;

        foreach (var item in mapperBody.GetComponentsInChildren<Collider>())
        {
            item.enabled = !disableCollider;
        }
        if (mapperBody.GetComponent<CharacterController>())
        {
            mapperBody.GetComponent<CharacterController>().enabled = !disableCollider;
        }
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
    Vector3 ogScale = new Vector3(-69, -69, -69);
    Vector3 scaleDiff = Vector3.one;
    void LocalFunctions()
    {
        //AudioFunctions();
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
                if (mapperBody == StartOfRound.Instance.localPlayerController)
                {
                    CustomEmotesAPI.localMapper = this;
                    local = true;
                }
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
                try
                {
                    currentClip.clip.ToString();
                    CustomEmotesAPI.Changed("none", this);
                    NewAnimation(null);
                    if (currentClip.syncronizeAnimation || currentClip.syncronizeAudio)
                    {
                        CustomAnimationClip.syncPlayerCount[currentClip.syncPos]--;
                    }
                    if (primaryAudioClips[currentClip.syncPos][currEvent] != null)
                    {
                        audioObject.GetComponent<AudioManager>().Stop(); //replace this with the audio manager eventually
                        if (primaryAudioClips[currentClip.syncPos][currEvent] != null && currentClip.syncronizeAudio)
                        {
                            listOfCurrentEmoteAudio[currentClip.syncPos].Remove(audioObject.GetComponent<AudioSource>());
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
        if (h <= 0)
        {
            UnlockBones(enableAnimatorOnDeath);
            GameObject.Destroy(gameObject);
        }
    }
    void WorldPropAndParent()
    {
        if (parentGameObject)
        {
            if (positionLock)
            {
                mapperBody.gameObject.transform.position = parentGameObject.transform.position + new Vector3(0, 1, 0);
                mapperBody.transform.position = parentGameObject.transform.position;
            }
            if (rotationLock)
            {
                mapperBody.transform.eulerAngles = parentGameObject.transform.eulerAngles + new Vector3(90, 0, 0);
            }
            if (scaleLock)
            {
                mapperBody.transform.localScale = new Vector3(parentGameObject.transform.localScale.x * scaleDiff.x, parentGameObject.transform.localScale.y * scaleDiff.y, parentGameObject.transform.localScale.z * scaleDiff.z);
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
        DebugClass.Log("Spawning Join Spot");
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
            EmoteNetworker.instance.SyncJoinSpot(mapperBody.GetComponent<NetworkObject>().NetworkObjectId, currentEmoteSpot.GetComponentInParent<NetworkObject>().NetworkObjectId, true, spot);
        }
        else
        {
            EmoteNetworker.instance.SyncJoinSpot(mapperBody.GetComponent<NetworkObject>().NetworkObjectId, currentEmoteSpot.GetComponentInParent<NetworkObject>().NetworkObjectId, false, spot);
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
    internal IEnumerator waitForTwoFramesThenDisableA1()
    {
        yield return new WaitForEndOfFrame(); //haha we only wait for one frame lmao
        a1.enabled = false;
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
                }
            }
            if (primaryAudioClips[currentClip.syncPos][currEvent] != null)
            {
                audioObject.GetComponent<AudioManager>().Stop();
                if (currentClip.syncronizeAudio)
                {
                    listOfCurrentEmoteAudio[currentClip.syncPos].Remove(audioObject.GetComponent<AudioSource>());
                }
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
        foreach (var smr in smr2)
        {
            for (int i = 0; i < smr.bones.Length; i++)
            {
                try
                {
                    if (smr.bones[i].gameObject.GetComponent<EmoteConstraint>())
                    {
                        smr.bones[i].gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
        cameraConstraint.DeactivateConstraints();
        a1.enabled = animatorEnabled;
    }
    public void LockBones()
    {
        foreach (var item in currentClip.soloIgnoredBones)
        {
            if (a2.GetBoneTransform(item))
                dontAnimateUs.Add(a2.GetBoneTransform(item).name);
        }
        foreach (var item in currentClip.rootIgnoredBones)
        {
            if (a2.GetBoneTransform(item))
                dontAnimateUs.Add(a2.GetBoneTransform(item).name);
            foreach (var bone in a2.GetBoneTransform(item).GetComponentsInChildren<Transform>())
            {

                dontAnimateUs.Add(bone.name);
            }
        }
        if (!jank)
        {
            //a1.enabled = false;
            StartCoroutine(waitForTwoFramesThenDisableA1());
            foreach (var smr in smr2)
            {
                for (int i = 0; i < smr.bones.Length; i++)
                {
                    try
                    {
                        if (smr.bones[i].gameObject.GetComponent<EmoteConstraint>() && !dontAnimateUs.Contains(smr.bones[i].name))
                        {
                            //DebugClass.Log($"-{i}---------{smr.bones[i].gameObject}");
                            smr.bones[i].gameObject.GetComponent<EmoteConstraint>().ActivateConstraints(); //this is like, 99% of fps loss right here. Unfortunate
                        }
                        else if (dontAnimateUs.Contains(smr.bones[i].name))
                        {
                            //DebugClass.Log($"dontanimateme-{i}---------{smr.bones[i].gameObject}");
                            smr.bones[i].gameObject.GetComponent<EmoteConstraint>().DeactivateConstraints();
                        }
                    }
                    catch (Exception e)
                    {
                        DebugClass.Log($"{e}");
                    }
                }
            }
            if ((currentClip.lockCameraByDefault || Settings.AllEmotesLockHead.Value) && !Settings.NoEmotesLockHead.Value)
            {
                cameraConstraint.ActivateConstraints();
            }
            else
            {
                cameraConstraint.DeactivateConstraints();
            }
        }
        else
        {
            //a1.enabled = false;

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
