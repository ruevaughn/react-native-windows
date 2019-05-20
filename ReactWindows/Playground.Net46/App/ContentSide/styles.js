import { StyleSheet } from 'react-native'

export default StyleSheet.create({
  content: {
    flexGrow: 1,
    flexDirection: 'column',
    backgroundColor: 'lightyellow',
    borderWidth: 1,
    borderColor: 'blanchedalmond'
  },
  item: {
    flex: 1
  },
  title: {
    fontSize: 25
  },
  caption: {
    fontSize: 20
  },
  subCaption: {
    fontSize: 16,
    fontWeight: '500'
  },
  autoCapOrdinar: {
    backgroundColor: 'lightyellow'
  },
  autoCapSelected: {
    borderWidth: 1,
    borderColor: 'maroon',
  },
  textInput: {
    flexDirection: 'row',
    justifyContent: 'flex-start',
    alignItems: 'flex-start',
    borderWidth: 1,
    borderColor: 'blanchedalmond',
    margin: 2
  },
  imageBox: {
    height: 32,
    width: 46
  },
  outTouchableOpacityStyle: {
    flex: 1,
    marginBottom: 5,
    height: '100%',
    borderBottomLeftRadius: 22,
    borderTopLeftRadius: 22,
    backgroundColor: '#0070A0'
  },
  innerViewStyle: {
    flex: 1,
    height: '100%',
    borderBottomLeftRadius: 22,
    borderTopLeftRadius: 22,
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: 'green',
  },
  container: {
    alignItems: 'center',
    justifyContent: 'center',
    marginHorizontal: 8,
    marginBottom: 4,
    marginTop: 2,
    width: 40,
    height: 40,
    backgroundColor: 'white',
    borderRadius: 16,
    borderWidth: 2,
    borderColor: 'black'
  },
})
