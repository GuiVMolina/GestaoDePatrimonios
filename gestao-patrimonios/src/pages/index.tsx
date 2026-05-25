export async function getServerSideProps() {
  return {
    redirect: {
      destination: "/home",
      performance: false,
    },
  };
}

export default function Index() {
  return null;
}