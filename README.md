# SettingsPlugins - Plugin Audio Unity 

Un plugin Unity complet pour gérer les paramètres audio, la musique de fond et les effets sonores avec un système d'interface utilisateur complet et un stockage persistant.

## 🎵 Fonctionnalités

### Gestion Audio
- **Système de Musique de Fond** : Lecture, pause et changement entre plusieurs pistes musicales
- **Effets Sonores (SFX)** : Lecture de SFX par clé ou AudioClip avec contrôle du volume
- **Fondu Musical** : Transitions fluides entre pistes musicales avec durée de fondu personnalisable
- **Système de File d'Attente** : Mise en file d'attente de plusieurs pistes pour une lecture séquentielle
- **Contrôle du Volume** : Contrôle indépendant pour les volumes de musique et SFX (plage 0-1)

### Système de Paramètres
- **Stockage Persistant** : Les paramètres sont automatiquement sauvegardés avec PlayerPrefs
- **Mises à Jour en Temps Réel** : Les changements de volume s'appliquent immédiatement pendant le jeu
- **Navigation des Pistes Musicales** : Boutons Précédent/Suivant pour parcourir les pistes disponibles
- **Interface des Paramètres** : Système d'interface complet avec curseurs et boutons de navigation

### Gestion des Scènes
- **Chargement Additif de Scènes** : L'interface des paramètres se charge dans une scène séparée sans interrompre le gameplay
- **Gestion des AudioListener** : Gère automatiquement les conflits AudioListener entre scènes
- **Intégration Facile** : API simple pour ouvrir/fermer les paramètres depuis n'importe où dans votre jeu

## 📁 Structure du Projet

```
SettingsPlugins/
├── Audios/
│   ├── Musics/          # Pistes de musique de fond
│   └── Sfxs/           # Clips d'effets sonores
├── Prefabs/            # Prefabs prêts à l'emploi
├── Scenes/             # Scènes de démonstration et de paramètres
└── Scripts/
    ├── Controller/     # Contrôleurs d'interface
    ├── Loader/         # Utilitaires de chargement de scènes
    ├── Manager/        # Gestionnaires audio et paramètres principaux
    └── Dummys/         # Implémentations d'exemple
```

## 🚀 Démarrage Rapide

### 1. Importer le Plugin

## 📦 Téléchargement

Vous pouvez télécharger le plugin directement sous forme de UnityPackage :

**Option A – Fichier UnityPackage (Recommandé)**  
👉 [Télécharger SettingsPlugins.unitypackage](https://drive.google.com/file/d/1a3foQoLAntNlRLS5pyWVg2mu-oSe6as-/view?usp=drive_link)

- Double-cliquez sur le fichier `.unitypackage` pour l’importer dans Unity
- Tous les assets, scripts et prefabs seront automatiquement importés
- Le plugin utilise le namespace `TheFlow.Audio`
"""
#### Option B : Import Manuel
- Copiez le dossier `SettingsPlugins` dans le dossier `Assets` de votre projet Unity
- Le plugin utilise le namespace `TheFlow.Audio`

### 2. Configurer les Sources Audio
1. Créez un GameObject vide dans votre scène
2. Ajoutez le composant `MusicManager`
3. Assignez des composants AudioSource pour la musique et les SFX
4. Ajoutez vos pistes musicales à la `musicList`
5. Ajoutez des entrées SFX avec des clés et des AudioClips

### 3. Configurer le Gestionnaire de Paramètres
1. Créez un GameObject vide dans votre scène
2. Ajoutez le composant `SettingsManager`
3. Le gestionnaire s'initialisera automatiquement comme singleton

### 4. Configurer l'Interface des Paramètres
1. Utilisez le prefab `Settings` fourni ou créez votre propre interface
2. Ajoutez le composant `SettingsController`
3. Assignez les éléments d'interface (curseurs, boutons) au contrôleur
4. Configurez le `SettingsLoader` pour gérer le chargement de scènes

### 5. Charger la Scène des Paramètres
```csharp
// Ouvrir les paramètres
SettingsLoader.Instance.OpenSettings();

// Fermer les paramètres
SettingsLoader.Instance.CloseSettings();
```

## 🎮 Exemples d'Utilisation

### Lecture de Musique
```csharp
// Lire de la musique par index
MusicManager.Instance.PlayMusicByIndex(0);

// Mettre en file d'attente des pistes musicales
MusicManager.Instance.QueueMusic(1);
MusicManager.Instance.QueueMusic(2);
MusicManager.Instance.PlayNextInQueue();
```

### Lecture d'Effets Sonores
```csharp
// Lire un SFX par clé
MusicManager.Instance.PlaySFX("Click");

// Lire un SFX par AudioClip
MusicManager.Instance.PlaySFX(myAudioClip);
```

### Gestion des Paramètres
```csharp
// Définir les volumes
SettingsManager.Instance.SetMusicVolume(0.8f);
SettingsManager.Instance.SetSFXVolume(0.6f);

// Changer de piste musicale
SettingsManager.Instance.ChangeMusic(1);  // Piste suivante
SettingsManager.Instance.ChangeMusic(-1);  // Piste précédente

// Écouter les changements de volume
SettingsManager.Instance.OnMusicVolumeChanged.AddListener(OnMusicVolumeChanged);
SettingsManager.Instance.OnSFXVolumeChanged.AddListener(OnSFXVolumeChanged);
```

## 🔧 Configuration

### Paramètres du Music Manager
- **Durée de Fondu** : Temps en secondes pour les transitions musicales (par défaut : 1,5s)
- **Liste de Musique** : Tableau d'objets AudioClip pour la musique de fond
- **Liste SFX** : Dictionnaire de paires clé-valeur pour les effets sonores

### Gestionnaire de Paramètres
- **Volumes par Défaut** : Les volumes de musique et SFX sont sauvegardés avec des valeurs par défaut de 0,5
- **Index Musical** : L'index de la piste musicale actuelle est conservé entre les sessions

### Chargeur de Paramètres
- **Nom de la Scène des Paramètres** : Configurez le nom de votre scène de paramètres (par défaut : "Settings")

## 🎨 Composants d'Interface

Le plugin inclut un système d'interface complet avec :
- **Curseurs de Volume** : Contrôle en temps réel des volumes de musique et SFX
- **Navigation Musicale** : Boutons Précédent/Suivant pour la sélection de pistes
- **Bouton de Sortie** : Fermer le panneau des paramètres
- **Design Réactif** : Fonctionne avec n'importe quel système de mise en page d'interface

## 📦 Téléchargements

### UnityPackage
- **[SettingsPlugins.unitypackage](link-to-unitypackage)** - Fichier UnityPackage complet pour une intégration facile
- Inclut tous les scripts, prefabs, scènes et assets audio
- Compatible avec Unity 2019.4 LTS et versions ultérieures



## 📋 Prérequis

- **Version Unity** : 2019.4 LTS ou supérieure
- **Plateformes** : Toutes les plateformes supportées par Unity
- **Dépendances** : Aucune dépendance externe requise

## 🔄 Système d'Événements

Le plugin utilise UnityEvents pour un couplage lâche :

```csharp
// Événements du SettingsManager
OnMusicVolumeChanged    // Déclenché quand le volume de musique change
OnSFXVolumeChanged      // Déclenché quand le volume SFX change
OnMusicChangeRequested  // Déclenché quand la piste musicale change

// S'abonner aux événements
SettingsManager.Instance.OnMusicVolumeChanged.AddListener(OnMusicVolumeChanged);
```

## 🎯 Bonnes Pratiques

1. **Pattern Singleton** : `MusicManager` et `SettingsManager` utilisent le pattern singleton pour un accès global
2. **DontDestroyOnLoad** : Les gestionnaires persistent à travers les changements de scène
3. **PlayerPrefs** : Les paramètres sont automatiquement sauvegardés et restaurés
4. **Gestion des AudioListener** : Le plugin gère automatiquement les conflits AudioListener
5. **Gestion d'Erreurs** : Inclut des messages d'avertissement pour les clés SFX manquantes

## 💡 Conseils d'Intégration

- **Utilisez le UnityPackage** : Pour une intégration rapide et sans erreur
- **Testez les Scènes de Démo** : Lancez les scènes incluses pour voir le plugin en action
- **Personnalisez les Prefabs** : Modifiez les prefabs fournis selon vos besoins
- **Vérifiez les Namespaces** : Assurez-vous que `TheFlow.Audio` est bien importé

## 🐛 Dépannage

### Problèmes Courants
- **Pas d'Audio** : Vérifiez les assignations AudioSource dans MusicManager
- **Paramètres Non Sauvegardés** : Assurez-vous que SettingsManager est présent dans la scène
- **Problèmes de Chargement de Scène** : Vérifiez que le nom de la scène de paramètres correspond dans SettingsLoader
- **Conflits AudioListener** : Le plugin désactive automatiquement les AudioListeners dans les scènes additives

### Informations de Débogage
Le plugin inclut des logs de débogage pour :
- Clés SFX manquantes
- Opérations de chargement de scène
- Changements de volume

## 📄 Licence

Ce plugin est fourni tel quel pour un usage éducatif et commercial.

## 🤝 Support

Pour les problèmes, questions ou contributions, veuillez consulter la documentation du projet ou créer un problème dans le dépôt.

---

**Fait avec ❤️ pour les développeurs Unity** 
