import Loading from "../Components/Loader/Loading";
import useAuth from "../Features/Auth/hooks/useAuth";

export default function HomePage() {
  const { auth } = useAuth();
  console.log("Current Auth State:", auth);
  return (
    <>
      <h1>HomePage</h1>
    </>
  );
}
