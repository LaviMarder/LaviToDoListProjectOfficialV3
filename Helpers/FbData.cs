using Android.App;
using Android.Gms.Extensions;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System.Threading.Tasks;

namespace LaviToDoListProjectOfficialV3.Helpers
{
    public class FbData
    {
        public readonly FirebaseAuth auth;
        public readonly FirebaseApp app;
        public FirebaseFirestore firestore;

        public FbData()
        {
            app = FirebaseApp.InitializeApp(Application.Context);
            if (app is null)
            {
                FirebaseOptions options = GetMyOptions();
                app = FirebaseApp.InitializeApp(Application.Context, options);
            }
            firestore = FirebaseFirestore.GetInstance(app);
            auth = FirebaseAuth.Instance;
        }
        private FirebaseOptions GetMyOptions()
        {
            return new FirebaseOptions.Builder().SetProjectId("lavitodolistprojectofficialv3")
                .SetApplicationId("lavitodolistprojectofficialv3")
                .SetApiKey("AIzaSyAEL0hDL2BHZKsTYFnTlh_1iRegW9bZaPc")
                .SetStorageBucket("lavitodolistprojectofficialv3.firebasestorage.app").Build();

        }
        public async Task CreateUser(string email, string password)
        {
            await auth.CreateUserWithEmailAndPassword(email, password);
        }
        public async Task SignIn(string email, string password)
        {
            await auth.SignInWithEmailAndPassword(email, password);
        }

        public string GetNewDocumentId(string cName)
        {
            DocumentReference dr = firestore.Collection(cName).Document();
            return dr.Id;
        }
        public Android.Gms.Tasks.Task GetCollection(string cName, string id)
        {
            return firestore.Collection(cName).Document(id).Get();
        }

        public Android.Gms.Tasks.Task GetCollection(string CollectionName)
        {
            return firestore.Collection(CollectionName).Get();
        }
        public void AddCollectionSnapShotListener(Activity activity, string cName)
        {
            firestore.Collection(cName).AddSnapshotListener((IEventListener)activity);
        }

        public Android.Gms.Tasks.Task DeleteFsDocument(string cName, string id)//asyncron
        {
            return firestore.Collection(cName).Document(id).Delete();
        }


    }
}